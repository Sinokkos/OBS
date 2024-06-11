using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data.Services
{
    public class OrderService : IOrderService
    {
        public readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsyncs(string userId, string userRole)
        {
            // Kullanıcı Id ve Rolune göre kayıt getirecek metot

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Book)
                .Where(o => o.UserId == userId)
                .ToListAsync();


            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            // ShoppingCart tarafındakileri VT tarafına gönderme

            // Sipariş kaydı oluşturuluyor.(Master kayıt)
            var order = new Order
            {
                UserId = userId,
                Email = userEmailAddress
            };

            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem
                {
                    Amount = item.Amount,
                    BookId = item.Book.Id,
                    Price = item.Book.Price,
                    OrderId = order.Id
                };

                await _context.OrderItems.AddAsync(orderItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}
