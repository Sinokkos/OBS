using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;
using Online_Book_Store.ViewModel;
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
            int authorId = 0;
            var authors = _context.Authors.ToList();
            string spaceAuthor = Regex.Replace(data.AuthorName, @"\s+", "").ToLower();

            foreach (var author in authors)
            {
                if (Regex.Replace(author.Name, @"\s+", "").ToLower() == spaceAuthor)
                {
                    authorId = author.Id;
                    break;
                }
            }
            if (authorId == 0)
            {
                await _context.Authors.AddAsync(new Author()
                {
                    Name = data.AuthorName,
                    ImageURL = "https://www.google.com/imgres?q=empty%20profile%20picture&imgurl=https%3A%2F%2Fwww.pikpng.com%2Fpngl%2Fm%2F80-805068_my-profile-icon-blank-profile-picture-circle-clipart.png&imgrefurl=https%3A%2F%2Fwww.pikpng.com%2Fpngvi%2FbJoTbo_my-profile-icon-blank-profile-picture-circle-clipart%2F&docid=AbBxfjxuwCmNiM&tbnid=mam3ybpF12JCRM&vet=12ahUKEwiqnImn69CGAxUzVvEDHXkkB_AQM3oECB4QAA..i&w=840&h=681&hcb=2&ved=2ahUKEwiqnImn69CGAxUzVvEDHXkkB_AQM3oECB4QAA"

                });
                await _context.SaveChangesAsync();
                var author = await _context.Authors
                    .OrderBy(c => c.Id)
                    .LastOrDefaultAsync();
                authorId = author.Id;
            }

            int publisherId = 0;
            var publishers = _context.Authors.ToList();
            string spacePublisher = Regex.Replace(data.PublisherName, @"\s+", "").ToLower();

            foreach (var publisher in publishers)
            {
                if (Regex.Replace(publisher.Name, @"\s+", "").ToLower() == spacePublisher)
                {
                    authorId = publisher.Id;
                    break;
                }
            }
            if (publisherId == 0)
            {
                await _context.Publishers.AddAsync(new Publisher()
                {
                    Name = data.PublisherName,
                    

                });
                await _context.SaveChangesAsync();
                var publisher = await _context.Publishers
                    .OrderBy(c => c.Id)
                    .LastOrDefaultAsync();
                authorId = publisher.Id;
            }
            // Önce Book data oluşturuluyor.
            var newBook = new Book()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                PublicationDate = data.PublicationDate,
                AuthorId = authorId,
                PublisherId = publisherId,
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
                dbBook.AuthorId = authorId;
                dbBook.PublisherId = publisherId;

                
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
    }
}