using Microsoft.AspNetCore.Mvc;
using Online_Book_Store.Data.Cart;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.ViewModels;

namespace Online_Book_Store.Controllers
{
    public class OrdersController : Controller
    {
        //65
        // Burada hazır varolan IBookService yapısını kullanalım

        private readonly IBooksService _booksService;
        private readonly ShoppingCart _shoppingCart;
        // 72
        private readonly IOrderService _orderService;

        public OrdersController(IBooksService booksService, ShoppingCart shoppingCart, IOrderService ordersService)
        {
            _booksService = booksService;
            _shoppingCart = shoppingCart;
            _orderService = ordersService; // 72
        }

        // 74
        public async Task<IActionResult> Index()
        {
            string userId = "";
            string userRole = "";

            var orders = await _orderService.GetOrdersByUserIdAndRoleAsyncs(userId, userRole);

            return View(orders);
        }




        public IActionResult ShoppingCart()
        {
            // benim ekranda göstereceğim cart itemlarını almam lazım.

            var items = _shoppingCart.GetShoppingCartItems();

            _shoppingCart.ShoppingCartItems = items;

            // Hazırlamış olduğumuz kendi ShoppingCartVM üzerine bu bilgileri alalım

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()

            };

            return View(response);
        }

        // 69
        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            // Hangi movie ile ilgili Order yapıldığını anlamak için
            var item = await _booksService.GetBookByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction("ShoppingCart");
        }

        // 70
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _booksService.GetBookByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction("ShoppingCart");
        }

        // 73
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems(); // Cart da neler var gelcek

            string userId = "";
            string userEmailAddress = "";

            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);

            // VT tarafına gönderdikten sonra sepeti boşaltalım...

            await _shoppingCart.ClearShoppinCartAsync();

            return View("OrderCompleted");
        }
    }
}
