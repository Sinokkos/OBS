using Online_Book_Store.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Online_Book_Store.ViewModel
{
    public class NewBookVM
    {
        // Yeni bir film yaratmak istediğimde kullanacağım model.
        // Movie modeline benzer şekilde CreateView tarafında kullanabilmek için

        [Key]
        public int Id { get; set; }

        [Display(Name = "Book Name")]
        [Required(ErrorMessage = "Kitap adı gerekli bilgidir...")]
        public string? Name { get; set; }

        [Display(Name = "Book Description")]
        [Required(ErrorMessage = "Kitap açıklaması gerekli bilgidir...")]
        public string? Description { get; set; }

        [Display(Name = "Price (TL)")]
        [Required(ErrorMessage = "Fiyat bilgisi gerekli bilgidir...")]
        public double Price { get; set; } // Bilet fiyatı

        [Display(Name = "Book Poster URL")]
        [Required(ErrorMessage = "Poster URL gerekli bilgidir...")]
        public string? ImageURL { get; set; } // Filmin afişi

        [Display(Name = "Book Published Date")]
        [Required(ErrorMessage = "Kitap basım tarihi gerekli bilgidir...")]
        
        public int PublicationDate { get; set; }
        // Film türü için

        [Display(Name = "Select a Category")]
        [Required(ErrorMessage = "Kitap kategorisi gerekli bilgidir...")]
        public BookCategory BookCategory { get; set; }

        // Relations (One-to-Many)
        // Burası çoktan seçmeli dropdownlist olarak gelecek
        

        // Burası çoktan seçmeli dropdownlist olarak gelecek
        [Display(Name = "Select Author")]
        [Required(ErrorMessage = "Yazar bilgisi gereklidir...")]
        public int AuthorId { get; set; }

        // Burası çoktan seçmeli dropdownlist olarak gelecek
        [Display(Name = "Select Publisher")]
        [Required(ErrorMessage = "Yayıncı bilgisi gereklidir...")]
        public int PublisherId { get; set; }

    }
}
