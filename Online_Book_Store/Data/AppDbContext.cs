using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Enum;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            

        }

        

        // VT tarafında oluşacak olan modele karşılık gelecek tablolar
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<Online_Book_Store.ViewModels.NewBookVM> NewBookVMs { get; set; } = default!;

    }
}
