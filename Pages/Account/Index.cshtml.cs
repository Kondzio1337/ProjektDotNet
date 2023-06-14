using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;

namespace Projekt.Pages.Account
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public IList<User> Users { get; set; } = default!;

        public IndexModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Users = _context.users.ToList();
        }
    }
}
