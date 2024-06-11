using Online_Book_Store.Models;

namespace Online_Book_Store.Data.Interfaces
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAndRoleAsyncs(string userId, string userRole);
    }
}
