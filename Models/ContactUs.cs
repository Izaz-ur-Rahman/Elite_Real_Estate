using System.ComponentModel.DataAnnotations;

namespace EliteRealEstate.Models
{
    public class ContactUs
    { 
        [Required]
        public string FirstName { get; set; }

        
        public string? LastName { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
