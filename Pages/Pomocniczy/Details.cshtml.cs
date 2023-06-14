using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Pomocniczy
{
    [Authorize(Policy = "kierownik")]
    public class DetailsModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public DetailsModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

      public Produkt Produkt { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt.FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }
            else 
            {
                Produkt = produkt;
            }
            return Page();
        }
    }
}
