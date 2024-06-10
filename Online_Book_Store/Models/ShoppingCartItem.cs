using System.ComponentModel.DataAnnotations;

namespace Online_Book_Store.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }


        public Book Book { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
