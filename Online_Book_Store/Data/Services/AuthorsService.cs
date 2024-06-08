using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>,IAuthorsService
    {
        public AuthorsService(AppDbContext context) : base (context)
        { 
        
        }
      
        
    }
}
