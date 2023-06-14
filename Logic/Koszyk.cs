using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projekt.Data;
using Projekt.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace Projekt.Logic
{
    public class ShoppingCartActions 
    {
        
       
        
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ShoppingCartActions(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

        public List<CartItem> GetShoppingCartItems()
        {
            var cartJson = _httpContextAccessor.HttpContext.Request.Cookies["ShoppingCart"];
            if (cartJson != null)
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
            }
            return new List<CartItem>();
        }

        public void SaveShoppingCartItems(List<CartItem> items)
            {
            var cartJson = JsonSerializer.Serialize(items);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7) // Ustaw ważność ciasteczka na 7 dni
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("ShoppingCart", cartJson, cookieOptions);
        }
            public void AddToCart(int productId, string productName, decimal price, int quantity)
        {
            var items = GetShoppingCartItems();
            var existingItem = items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity
                };
                items.Add(newItem);
            }

            SaveShoppingCartItems(items);
        }

        public void RemoveFromCart(int productId)
        {
            var items = GetShoppingCartItems();
            var itemToRemove = items.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                SaveShoppingCartItems(items);
            }
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext.Session.Remove("ShoppingCartItems");
        }

    }
}