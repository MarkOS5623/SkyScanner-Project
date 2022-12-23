using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace SkyScanner.Models
{
    public class Seat
    {
        [Key]
        [StringLength(10)]
        public string Seat_Num { get; set; }
        public string Flight_num { get; set; }
        [ForeignKey("Flight_num")]
        public virtual Flight Flight { get; set; }
        public bool Booked { get; set; } = false;
        public Seat()
        {
                
        }
        public Seat(string num)
        {
            Seat_Num = num;
        }
    }
}
