using Online_Book_Store.Data.Base;
using Online_Book_Store.Data.Enum;

namespace Online_Book_Store.Models
{
    public class Book : IEntityBase
    {
        internal BookCategory BookCategory;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public double Price { get; set; }

        public int PublicationDate { get; set; }
        
        public BookCategory Category { get; set; }

        // Relation 
        // Author
        public Author Author { get; set; }

        public int AuthorId { get; set; }

        // Publisher
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
        
    }
}
