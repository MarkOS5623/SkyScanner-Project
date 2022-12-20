using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkyScanner.Models
{
    public class Admin
    {
        [Key]
        [Required]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "ID must only contain numbers and have 9 characters")]
        [StringLength(9, MinimumLength = 9)]
        public string AdminId { get; set; }
    }
}
