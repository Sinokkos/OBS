using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Services;
using Online_Book_Store.Models;
using Online_Book_Store.ViewModel;

namespace Online_Book_Store.Data.Interfaces
{
    public interface IBooksService : IEntityBaseRepository<Book>
    {
        
        Task<Book> GetBookByIdAsync (int id);

        Task AddNewBookAsync(NewBookVM data);

        Task UpdateBookAsync(NewBookVM data);

        Task DeleteBookAsync(int id);

        Task<NewBookDropdownsVM> GetNewBookDropdownsValues();
    }
}
