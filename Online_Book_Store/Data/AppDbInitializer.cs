using Online_Book_Store.Models;
using Online_Book_Store;
using Online_Book_Store.Data.Enum;

namespace Online_Book_Store.Data
    // Fake dummy
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder) 
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // Book tablosu için 
                

                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            Name = "J.K. Rowling",
                            ImageURL="https://n24.com.tr/wp-content/uploads/2023/04/Harry-Potter-JK-Rowling.jpg",
                        },

                    });

                    context.SaveChanges();
                }

                if (!context.Publishers.Any())
                {
                    context.Publishers.AddRange(new List<Publisher>()
                    {
                        new Publisher()
                        {
                            Name = "Bloomsbury Childrens Books",
                            
                        },

                    });

                    context.SaveChanges();
                }

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    { 
                         // 1. Kayıt
                         new Book()
                         {
                             Name = "Harry Potter philosopher's stone",
                             ImageURL= "https://m.media-amazon.com/images/I/81q77Q39nEL._AC_UF894,1000_QL80_.jpg",
                             Description="Büyülendiniz...",
                             Price= 200,
                             PublicationDate= 1997,
                             Category= BookCategory.Fantasy,
                             AuthorId= 1,
                             PublisherId= 1,
                         },

                         new Book()
                         {
                             Name = "IT",
                             ImageURL= "https://i.ebayimg.com/images/g/rgkAAOSw969f~26A/s-l500.jpg",
                             Description="Büyülendiniz...",
                             Price= 200,
                             PublicationDate= 1997,
                             Category= BookCategory.Horror,
                             AuthorId= 1,
                             PublisherId= 1,
                         },


                         new Book()
                         {
                             Name = "Harry Potter philosopher's stone",
                             ImageURL= "https://m.media-amazon.com/images/I/81q77Q39nEL._AC_UF894,1000_QL80_.jpg",
                             Description="Büyülendiniz...",
                             Price= 200,
                             PublicationDate= 1997,
                             Category= BookCategory.Drama,
                             AuthorId= 1,
                             PublisherId= 1,
                         },

                         new Book()
                         {
                             Name = "IT",
                             ImageURL= "https://i.ebayimg.com/images/g/rgkAAOSw969f~26A/s-l500.jpg",
                             Description="Büyülendiniz...",
                             Price= 200,
                             PublicationDate= 1997,
                             Category= BookCategory.Education,
                             AuthorId= 1,
                             PublisherId= 1,
                         },
                         new Book()
                         {
                             Name = "IT",
                             ImageURL= "https://i.ebayimg.com/images/g/rgkAAOSw969f~26A/s-l500.jpg",
                             Description="Büyülendiniz...",
                             Price= 200,
                             PublicationDate= 1997,
                             Category= BookCategory.Fantasy,
                             AuthorId= 1,
                             PublisherId= 1,
                         }


                    });

                    context.SaveChanges();

                }
            }
        }
    }
}
