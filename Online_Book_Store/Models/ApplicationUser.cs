using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Online_Book_Store.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]

        public string FullName { get; set; }
    }
}
