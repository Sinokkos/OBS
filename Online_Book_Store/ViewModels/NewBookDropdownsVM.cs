using Online_Book_Store.Models;

namespace Online_Book_Store.ViewModels
{
    public class NewBookDropdownsVM
    {
        public List<Author> Authors {get; set; }

        public List<Publisher> Publishers{get; set; }
   
        public NewBookDropdownsVM() 
        {
          Authors= new List<Author>();
     
          Publishers= new List<Publisher>();
        
        }
    }
     
}
