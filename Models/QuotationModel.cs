using System.ComponentModel.DataAnnotations;

namespace EliteRealEstate.Models
{
    public class QuotationModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Budget { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
