using System.ComponentModel.DataAnnotations;
namespace SkyScanner.Models
{
    public class Seat
    {
        [Key]
        [StringLength(5, MinimumLength = 5)]
        public string Seat_Num { get; set; }
        public bool Booked { get; set; }
    }
}
