using Microsoft.EntityFrameworkCore;
using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Interfaces;
using Online_Book_Store.Models;

namespace Online_Book_Store.Data.Services
{
    public class PublishersService : EntityBaseRepository<Publisher>, IPublishersService
    {
        public PublishersService(AppDbContext context) : base(context)
        {

        }


    }
}
