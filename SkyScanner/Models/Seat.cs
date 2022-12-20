using System.ComponentModel.DataAnnotations;
namespace SkyScanner.Models
{
    public class Seat
    {
        [Key]
        [StringLength(5, MinimumLength = 5)]
        public string Seat_Num { get; set; }
        [StringLength(20)]
        public string Class_Type { get; set; }
        public bool Booked { get; set; }
    }
}
