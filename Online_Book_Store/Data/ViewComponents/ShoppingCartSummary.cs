﻿using Microsoft.AspNetCore.Mvc;
using Online_Book_Store.Data.Cart;
using Online_Book_Store.ViewModels;

namespace Online_Book_Store.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        // 74.
        // Normalde ShoppingCartSummaryViewComponents olarak da isi verilebilirdi . Fakat biz ismi kısa tutup buranın bir ViewComponent olduğunu kod içersinde belirteceğiz.

        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        // Default Method. Buranın çağırılabilmesi için
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            _shoppingCart.ShoppingCartItems = items; // bana gelenler

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(items.Count);



        }
    }
}
