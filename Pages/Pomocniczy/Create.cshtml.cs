using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Pomocniczy
{
    [Authorize(Policy = "kierownik")]
    public class CreateModel : PageModel
    {
        private readonly Projekt.Data.ProjektContext _context;

        public CreateModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<string> SelectedCategories { get; set; }

        public List<SelectListItem> AvailableCategories { get; set; }
        public IActionResult OnGet()
        {
            var categories = _context.kategorie.ToList();

            // Konwertuj kategorie na listę SelectListItem
            AvailableCategories = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            }).ToList();
            return Page();
        }

        [BindProperty]
        public Produkt Produkt { get; set; } = default!;


            // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
            public async Task<IActionResult> OnPostAsync()
        {
          if ( Produkt == null||SelectedCategories==null)
            {
                return Page();
            }

            _context.Produkt.Add(Produkt);
            await _context.SaveChangesAsync();

            foreach(var categoryId in SelectedCategories)
            {
                var connector = new Connector
                {
                    ProduktId = Produkt.Id,
                    KategoriaId = int.Parse(categoryId)
                };
                _context.connectors.Add(connector);

            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
