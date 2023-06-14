using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Logic;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Pages.Account
{
    [Authorize(Policy = "admin")]
    public class DodawanieKontaModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public DodawanieKontaModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User Uzytkownik { get; set; } = default!;
        [BindProperty]
        public string haslo2 { get; set; }

       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Uzytkownik == null)
            {
                return Page();
            }
            if (Uzytkownik.Password != haslo2)
            {
                ModelState.AddModelError(string.Empty, "Wpisane has³a nie s¹ identyczne.");
                return Page();
            }
            if (_context.users.Any(u => u.Login == Uzytkownik.Login))
            {
                ModelState.AddModelError(string.Empty, "Taki Login jest ju¿ zajêty.");
                return Page();
            }
            //string haslotym = HashHaslo.HashPassword(Uzytkownik.Password);
            //string[] parts = haslotym.Split(':');
            Uzytkownik.Password = HashHaslo.HashPassword(Uzytkownik.Password);
            //Uzytkownik.Salt = parts[1];
            // Wywo³anie procedury sk³adowanej
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC DODAJUSER @Login={Uzytkownik.Login}, @Password={Uzytkownik.Password},@IdRoli={Uzytkownik.IdRoli}");

            return RedirectToPage("/Account/Index");
            /*

            if (!ModelState.IsValid || _context.users == null || Uzytkownik == null)
            {
                return Page();
            }
            if (Uzytkownik.Password != haslo2)
            {
                ModelState.AddModelError(string.Empty, "Wpisane has³a nie s¹ identyczne.");
                return Page();
            }
            if (_context.users.Any(u => u.Login == Uzytkownik.Login))
            {
                ModelState.AddModelError(string.Empty, "Taki Login jest juz zajety.");
                return Page();
            }

            _context.users.Add(Uzytkownik);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account/Index");
            */
        }
    }
}
