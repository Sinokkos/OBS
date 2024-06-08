using Online_Book_Store.Data.Base;

namespace Online_Book_Store.Models
{
    public class Publisher : IEntityBase
    {
        public int Id { get; set; }     
        public string Name { get; set; }    
        public List<Book> Books { get; set; }
    }
}
