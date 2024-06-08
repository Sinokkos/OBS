using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;
using Online_Book_Store.ViewModel;

namespace Online_Book_Store.Data.Services
{
    public class BooksService : EntityBaseRepository<Book>, IBooksService
    {
        private readonly AppDbContext _context;

        public BooksService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // 40.Movie 
        public async Task<Book> GetBookByIdAsync(int id)
        {
            // Burada ilişkili yapı için ayrı bir metot devreye giriyor.

            var bookDetails = _context.Books
                .Include(a => a.Author) // İlişki
                .Include(p => p.Publisher)
                .FirstOrDefault(n => n.Id == id);

            return bookDetails;
        }


        public async Task AddNewBookAsync(NewBookVM data)
        {
            // Önce Book data oluşturuluyor.
            var newBook = new Book()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                PublicationDate = data.PublicationDate,
                AuthorId = data.AuthorId,
                PublisherId = data.PublisherId,
                BookCategory = data.BookCategory
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            
            
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = _context.Books.FirstOrDefault(n => n.Id == data.Id);

            if (dbBook != null)
            {
                // update kısmı
                dbBook.Name = data.Name;
                dbBook.Description = data.Description;
                dbBook.Price = data.Price;
                dbBook.ImageURL = data.ImageURL;
                dbBook.PublicationDate = data.PublicationDate;
                dbBook.BookCategory = data.BookCategory;
                dbBook.AuthorId = data.AuthorId;
                dbBook.PublisherId = data.PublisherId;

                
            }
                                    
            await _context.SaveChangesAsync();

        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            // Create ekranında bulunacak dropdownlar için listeler oluşturacak...

            var response = new NewBookDropdownsVM()
            {
                Authors = await _context.Authors
                     .OrderBy(n => n.Name).ToListAsync(),
                Publishers = await _context.Publishers.OrderBy(n => n.Name).ToListAsync(),
                

            };

            return response;
        }

        
    }
}