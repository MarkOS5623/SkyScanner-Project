using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkyScanner.Models
{
    public class Flight
    {
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Origin name must be between 4 and 30 charaters long")]
        public string Origin { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Destination name must be between 4 and 30 charaters long")]
        public string Destination { get; set; }
        [Required]
        public double Price { get; set; } = 0;
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Airline name must be between 4 and 30 charaters long")]
        public string Airline { get; set; }
        [Key]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Flight ID must only contain numbers and have 4 characters")]
        [StringLength(4, MinimumLength = 4)]
        public string FlightId { get; set; }
    }
}