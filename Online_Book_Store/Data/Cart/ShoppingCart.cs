﻿using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        // 67
        // Shopping Cart direkt getiren metot
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            // Session yapısını yerleştiriyoruz
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var _context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();// eğer CartId tanımında bir session olarak bulamıyorsan yeni bir tane Guid olarak oluştur.

            session.SetString("CartId", cartId); //Seesion a yeni değeri yerleştir.

            return new ShoppingCart(_context) { ShoppingCartId = cartId };

        }



        // ShoppingCart içinde bulunan itemları getiren metot.
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            // LINQ sorgusu ile
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                        .Where(c => c.ShoppingCartId == ShoppingCartId)
                        .Include(s => s.Book) //join gibi düşünebiliriz
                        .ToList());

        }

        // ShoppingCart ın toplamını getirecek olan metot
        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Book.Price * c.Amount) // bir filme kac adet bilet almışım
                .Sum();

            return total;
        }

        // Sepete bir item ekleyecek metot
        public void AddItemToCart(Book book)
        {
            // Öncelikle sepette ilgili Movie ile aynı Id li bir kayıt var mı / yokmu kontrolu
            // eğer yoksa ilk sepet elemanını oluşturacak
            // eğer varsa varolan miktarı artıracak.

            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Book.Id == book.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            { // İlgili movie ile ilgili bir kayıt yoksa
                // bir tane kayıt oluştur.
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1 // ilk bilet
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                // Eğer Movie ile ilgili bir kayıda rastlamış ise

                shoppingCartItem.Amount++; // Bir bilet daha ekle
            }

            _context.SaveChanges();
        }

        // Sepetten bir item kaldıracak metot
        public void RemoveItemFromCart(Book book)
        {
            var shoppingCartItem = _context.ShoppingCartItems
    .FirstOrDefault(n => n.Book.Id == book.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                // Eğer varsa

                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--; // Sepetten çıkarttım
                }
                else
                {
                    // Sadece tek biletim varsa
                    _context.ShoppingCartItems.Remove(shoppingCartItem);

                }
            }

            _context.SaveChanges();

        }

        // 73
        public async Task ClearShoppinCartAsync()
        {
            var items = await _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .ToListAsync();


            _context.ShoppingCartItems.RemoveRange(items);

            await _context.SaveChangesAsync();
        }
    }
}
