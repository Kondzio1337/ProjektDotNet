using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Logic;
using Projekt.Models;

namespace Projekt.Pages.Koszyk
{
    
    public class DeleteModel : PageModel
    {
        
        public ShoppingCartActions ShoppingCart { get; set; }
       

        public DeleteModel(ShoppingCartActions SCart)
        {
            ShoppingCart = SCart;
           

        }



        public async Task<IActionResult> OnGet(int id)
        {
            ShoppingCart.RemoveFromCart(id);



            return RedirectToPage("/Koszyk/Index");

        }
        
    }
}
