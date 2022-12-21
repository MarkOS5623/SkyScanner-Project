using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkyScanner.Models
{
    public class User
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [StringLength(50, MinimumLength = 4)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 20 charaters long")]
        public string Password { get; set; }
        [Key]
        [Required]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "ID must only contain numbers and have 9 characters")]
        [StringLength(9, MinimumLength = 9)]
        public string UserId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "First name must be between 3 and 20 charaters long")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Last name must be between 3 and 20 charaters long")]
        public string LastName { get; set; }
        [Required]
        public bool Admin { get; set; } = false;
        [Required]
        public bool KeepLoggedIn { get; set; } = false;

    }
}
