using System.ComponentModel.DataAnnotations;

namespace EliteRealEstate.Models
{
    public class ContactUs
    { 
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
