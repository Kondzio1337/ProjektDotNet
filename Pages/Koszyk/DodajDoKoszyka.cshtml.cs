using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Projekt.Data;
using Projekt.Logic;
using Projekt.Models;

namespace Projekt.Pages.Koszyk
{
    public class DodajDoKoszykaModel : PageModel
    {
       public ShoppingCartActions ShoppingCart { get; set; }
        public ProjektContext _context { get; set; }
        public List<CartItem> lista { get; set; }
        public DodajDoKoszykaModel(ShoppingCartActions shop, ProjektContext CONTEXT)
        {

            ShoppingCart = shop;
            _context = CONTEXT;
        }
       
        public async Task<IActionResult> OnGet(int id)
        {
            var quantity = 1;
            lista = ShoppingCart.GetShoppingCartItems();
            var existingItem = lista.FirstOrDefault(u => u.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                ShoppingCart.SaveShoppingCartItems(lista);
            }
            else
            {
                var productName = _context.Produkt.FirstOrDefault(p => p.Id == id)?.Name;
                var price = _context.Produkt.FirstOrDefault(p => p.Id == id)?.Price;
                ShoppingCart.AddToCart(id, productName, price.Value, quantity);
            }
           
            return RedirectToPage("/Koszyk/Index");
        }
    }
}
