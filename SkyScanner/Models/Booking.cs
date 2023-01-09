using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SkyScanner.Models
{
    public class Booking
    {
        public Booking()
        {
                
        }
        public Booking(string origin, string destination, string user_ID, User user, string bookingID, string? bookedSeats, bool twoWay)
        {
            Origin = origin;
            Destination = destination;
            User_ID = user_ID;
            User = user;
            BookingID = bookingID;
            BookedSeats = bookedSeats;
            TwoWay = twoWay;
        }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Origin name must be between 4 and 30 charaters long")]
        public string Origin { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Destination name must be between 4 and 30 charaters long")]
        public string Destination { get; set; }
        [RegularExpression("^[0-9]{4}$")]
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual User User { get; set; }
        [Key]
        [Required]
        [RegularExpression("^[0-9]{4}$")]
        [StringLength(4, MinimumLength = 4)]
        public string BookingID { get; set; }
        [Required]
        [StringLength(1000)]
        public string? BookedSeats { get; set; }
        [DisplayName("Two Way")]
        public bool TwoWay { get; set; } = false;

    }
}
