using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Pomocniczy
{

    public class IndexModel : PageModel
    {
        public List<SelectListItem> Kategorie { get; set; }


        public string WartośćSearchString { get; set; }
        public string ID1 { get; set; }

        private readonly Projekt.Data.ProjektContext _context;

        public IndexModel(Projekt.Data.ProjektContext context)
        {
            _context = context;
        }

        public IList<Produkt> Produkt { get; set; } = default!;
        public IList<Connector> Connectors { get; set; } = default!;



        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public string? ID { get; set; }
        public SelectList? Kategoria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Nazwa { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            MojaAkcja();

            WartośćSearchString = Request.Form["WartośćSearchString"];
            ID1 = Request.Form["ID"];

           

            if (!string.IsNullOrEmpty(ID1))
            {
                string link1 = "https://localhost:7048/Pomocniczy";
                string encodedSearchString1 = Uri.EscapeDataString(ID1);
                string caly1 = $"{link1}?searchString=&&ID={encodedSearchString1}";
                return Redirect(caly1);
            }
            if (string.IsNullOrEmpty(WartośćSearchString))
            {
                return RedirectToPage("/Index");
            }

            string link = "https://localhost:7048/Pomocniczy";
            string encodedSearchString = Uri.EscapeDataString(WartośćSearchString);
            string caly = $"{link}?searchString={encodedSearchString}";
            return Redirect(caly);
        }


        public async Task OnGetAsync(string? searchString,string? ID)
        {
            var produkt = from m in _context.Produkt
                          select m;
            var cone = from m in _context.connectors
                          select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                produkt = produkt.Where(s => s.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ID))
            {
                int categoryId = int.Parse(ID);
                cone = cone.Where(p => p.KategoriaId == categoryId);
                
              
                produkt = produkt.Where(p => cone.Any(c => c.ProduktId == p.Id));

               

            }

            Produkt = await produkt.ToListAsync();

            Kategorie = await _context.kategorie.Select(k => new SelectListItem
            {
                Value = k.CategoryID.ToString(),
                Text = k.CategoryName
            }).ToListAsync();

        }
        public IActionResult MojaAkcja()
        {
            var kategorie = _context.kategorie.ToList();
            var options = kategorie.Select(k => new SelectListItem { Value = k.CategoryID.ToString(), Text = k.CategoryName }).ToList();

            Console.WriteLine("Liczba kategorii: " + options.Count);
            foreach (var kategoria in options)
            {
                Console.WriteLine("Kategoria: " + kategoria.Text);
            }
            return Page();
        }
    }

}
