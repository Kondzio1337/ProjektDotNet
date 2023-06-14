using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Projekt.Logic;

namespace Projekt.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        private readonly ProjektContext _dbContext;
        public LoginModel(ProjektContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() {
            var user = await _dbContext.users.FirstOrDefaultAsync(u => u.Login == Credential.login);
            if (!ModelState.IsValid) return Page();

            if (Credential.login == "admin" && Credential.haslo == "123") {

                var claims = new List<Claim> { 
                    new Claim (ClaimTypes.Name,"admin"),
                    new Claim ("Department","Admin"),
                    new Claim("Department", "Kierownik"),
                    new Claim("Department", "Uzytkownik")
                };
                var identity = new ClaimsIdentity(claims,"CiastkoAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                

                await HttpContext.SignInAsync("CiastkoAuth",claimsPrincipal);

                return RedirectToPage("/Index");
            }else if (user != null && HashHaslo.VerifyPassword(Credential.haslo, user.Password))
                {
                var claims = new List<Claim>();
                var identity = new ClaimsIdentity(claims, "CiastkoAuth");
                ClaimsPrincipal claimsPrincipal;

                switch (user.IdRoli)
                {
                    case 1:
                        claims = new List<Claim>
                                {
                                new Claim(ClaimTypes.Name, user.Login),
                                new Claim("Department", "Admin"),
                                new Claim("Department", "Kierownik"),
                                new Claim("Department", "Uzytkownik")
                                };
                        identity = new ClaimsIdentity(claims, "CiastkoAuth");
                        claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("CiastkoAuth", claimsPrincipal);
                        break;

                    case 2:
                        claims = new List<Claim>
                                {
                                new Claim(ClaimTypes.Name, user.Login),
                                new Claim("Department", "Kierownik"),
                                new Claim("Department", "Uzytkownik")
                                };
                        identity = new ClaimsIdentity(claims, "CiastkoAuth");
                        claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("CiastkoAuth", claimsPrincipal);
                        break;
                    case 3:
                        claims = new List<Claim>
                                {
                                new Claim(ClaimTypes.Name, user.Login),
                                new Claim("Department", "Uzytkownik")
                                };
                        identity = new ClaimsIdentity(claims, "CiastkoAuth");
                        claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("CiastkoAuth", claimsPrincipal);
                        break;


                    default:
                        return Page();
                        
                }


                return RedirectToPage("/Index");
                    
                }
                return Page();
        }

    }
    public class Credential {
        [Required]
        [Display(Name ="Nazwa U¿ytkownika")]
    public string login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Has³o")]
        public string haslo { get; set;}     
    
    }
}
