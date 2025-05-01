using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EliteRealEstate.Models
{
    public class EmailSendingModel
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

    }
}