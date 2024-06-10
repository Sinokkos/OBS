using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Book_Store.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public int BookId { get; set; }

        // Relation 
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        // 58
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; } // Orders tablosuna referans veriyoruz
    }
}
