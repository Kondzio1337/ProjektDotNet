using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Logic;
using Projekt.Models;

namespace Projekt.Pages.Koszyk
{
    [Authorize(Policy = "uzytkownik")]
    public class IndexModel : PageModel
    {
  
        private readonly ShoppingCartActions _koszyk;
        public IList<CartItem> Items { get; set; } = default!;

        public IndexModel(ShoppingCartActions koszykService)
        {
            _koszyk = koszykService;
        }

        


        public void OnPost()
        {
            _koszyk.ClearCart();
        }
        public void OnGet()
        {
            Items = _koszyk.GetShoppingCartItems(); // Pobierz dane koszyka
        }
    }
}
