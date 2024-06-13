using Online_Book_Store.Models;
using Online_Book_Store;
using Online_Book_Store.Data.Enum;
using Microsoft.AspNetCore.Identity;
using Online_Book_Store.Data.Static;
using Humanizer;
using System.Drawing;

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

                        new Author()
                        {
                            Name = "Stephen King",
                            ImageURL="https://cdn.britannica.com/20/217720-050-857D712B/American-novelist-Stephen-King-2004.jpg",
                        },

                        new Author()
                        {
                            Name = "Khaled Hosseini",
                            ImageURL="https://images.405magazine.com/wp-content/uploads/2022/02/Khaled-Hosseini.jpg",
                        },

                        new Author()
                        { 
                          Name = "fyodor dostoevsky",
                          ImageURL ="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTM5DwJbs6tyoRFAlkfrcw7eDBAH21OcXi9ag&s"
                        },

                        new Author()
                        {
                          Name = "antoine de saint-exupéry",
                          ImageURL ="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR4Vyu51FC-Dry8c6JFTnfG8XzfBAoVPzVfvg&s"
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

                        new Publisher()
                        { 
                           Name = "Altın Kitaplar"
                        
                        },

                        new Publisher()
                        { 
                          Name = "Engage Classics"
                        },

                        new Publisher()
                        {
                          Name = "Harcourt, Inc."
                        }

                       

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
                             Price= 100,
                             PublicationDate= 1997,
                             Category= BookCategory.Fantasy,
                             AuthorId= 1,
                             PublisherId= 1,
                         },

                         new Book()
                         {
                             Name = "IT",
                             ImageURL= "https://i.ebayimg.com/images/g/rgkAAOSw969f~26A/s-l500.jpg",
                             Description="Balonlara Dikkat",
                             Price= 400,
                             PublicationDate= 200,
                             Category= BookCategory.Horror,
                             AuthorId= 2,
                             PublisherId= 2,
                         },


                         new Book()
                         {
                             Name = " The Kite Runner",
                             ImageURL= "https://m.media-amazon.com/images/I/81IzbD2IiIL._AC_UF1000,1000_QL80_.jpg",
                             Description="Göklere...",
                             Price= 150,
                             PublicationDate= 2007,
                             Category= BookCategory.Drama,
                             AuthorId= 3,
                             PublisherId= 1,
                         },

                         new Book()
                         {
                             Name = "Le Petit Prince",
                             ImageURL= "https://m.media-amazon.com/images/I/710wth0vXZL._AC_UF894,1000_QL80_.jpg",
                             Description="Bazen Tilkiler En İyi Arkadaşınızdır...",
                             Price= 300,
                             PublicationDate= 1943,
                             Category= BookCategory.Kids,
                             AuthorId= 1,
                             PublisherId= 4,
                         },
                         new Book()
                         {
                             Name = "Crime and Punishment",
                             ImageURL= "https://m.media-amazon.com/images/I/71muc07-IPL._SY466_.jpg",
                             Description="Büyülendiniz...",
                             Price= 75,
                             PublicationDate= 1866,
                             Category= BookCategory.Literature,
                             AuthorId= 1,
                             PublisherId= 4,
                         }


                    });

                    context.SaveChanges();

                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!roleManager.RoleExistsAsync(UserRoles.Admin).Result)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!roleManager.RoleExistsAsync(UserRoles.User).Result)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                // Users
                var usersManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string adminUserEMail = "admin@OBS.com";

                var adminUser = usersManager.FindByEmailAsync(adminUserEMail).Result;

                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEMail,
                        EmailConfirmed = true
                    };
                    // * Şifre bir buyuk harf, sayı, özel karakter istiyormuş
                    await usersManager.CreateAsync(newAdminUser, "S1n3m.");

                    await usersManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEMail = "user@OBS.com";

                var appUser = usersManager.FindByEmailAsync(appUserEMail);

                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEMail,
                        EmailConfirmed = true
                    };

                    await usersManager.CreateAsync(newAppUser, "S1n3m*");

                    await usersManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
