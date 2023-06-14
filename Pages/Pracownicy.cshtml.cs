using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt.Pages
{
    [Authorize(Policy = "Kierownik")]
    public class PracownicyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
