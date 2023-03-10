using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkyScanner.Models
{
    public class User
    {
        public User()
        {

        }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [StringLength(50, MinimumLength = 4)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 20 charaters long")]
        public string Password { get; set; }
        [Key]
        [Required]
        [RegularExpression("^[0-9]{4}$")]
        [StringLength(4, MinimumLength = 4)]
        public string UserId { get; set; }
        [Required]
        public bool Admin { get; set; } = false;
        [Required]
        public bool KeepLoggedIn { get; set; } = false;
        [Required]
        public List<CreditCard> CreditCards { get; set; }
        [Required]
        public List<Booking> Bookings { get; set; }
    }
}
