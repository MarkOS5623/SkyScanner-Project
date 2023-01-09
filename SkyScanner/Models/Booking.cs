using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SkyScanner.Models
{
    public class Booking
    {
        public Booking()
        {
                
        }
        public string Origin { get; set; }
        public string Destination { get; set; }
        [RegularExpression("^[0-9]{4}$")]
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual User User { get; set; }
        [Key]
        [Range(1000,9999)]
        public int BookingID { get; set; }
        [Required]
        [StringLength(1000)]
        public string? BookedSeats { get; set; }
        public bool TwoWay { get; set; } = false;

    }
}
