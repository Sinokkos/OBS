using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;
using Online_Book_Store.ViewModels;
using System.Text.RegularExpressions;

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
                AuthorId = await GetAuthor(data.AuthorName),
                PublisherId = await GetPublisher(data.PublisherName),
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
                dbBook.AuthorId = await GetAuthor(data.AuthorName);
                dbBook.PublisherId = await GetPublisher(data.PublisherName);

                
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

        public Task DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<int> GetAuthor(string authorName)
        {
            var authors = await _context.Authors.ToListAsync();
            var author = authors.FirstOrDefault(
                c => Regex.Replace(c.Name, @"\s+", "").ToLower() == Regex.Replace(authorName, @"\s+", "").ToLower());
            if (author == null)
            {
                await _context.Authors.AddAsync(new Author()
                {
                    Name = authorName,
                    ImageURL = "",
                    
                });
                await _context.SaveChangesAsync();
                author = await _context.Authors
                    .OrderBy(c => c.Id)
                    .LastOrDefaultAsync();
                return author.Id;
            }
            return author.Id;

        }

        public async Task<string> GetAuthor(int authorId)
        {
            string authorName;
            var authors = await _context.Authors.ToListAsync();
            authorName = authors.FirstOrDefault(c => c.Id == authorId).Name;
            return authorName;
        }
        public async Task<int> GetPublisher(string publisherName)
        {
            var publishers = await _context.Publishers.ToListAsync();
            var publisher = publishers.FirstOrDefault(
                c => Regex.Replace(c.Name, @"\s+", "").ToLower() == Regex.Replace(publisherName, @"\s+", "").ToLower());
            if (publisher == null)
            {
                await _context.Publishers.AddAsync(new Publisher()
                {
                    Name = publisherName,
                    

                });
                await _context.SaveChangesAsync();
                publisher = await _context.Publishers
                    .OrderBy(c => c.Id)
                    .LastOrDefaultAsync();
                return publisher.Id;
            }
            return publisher.Id;

        }

        public async Task<string> GetPublisher(int publisherId)
        {
            string publisherName;
            var publishers = await _context.Authors.ToListAsync();
            publisherName = publishers.FirstOrDefault(c => c.Id == publisherId).Name;
            return publisherName;
        }
    }
}