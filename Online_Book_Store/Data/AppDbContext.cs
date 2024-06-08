using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Enum;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // VT tarafında oluşacak olan modele karşılık gelecek tablolar
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

      
    }
}
