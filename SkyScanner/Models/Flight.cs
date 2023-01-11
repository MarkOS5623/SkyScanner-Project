using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SkyScanner.Models
{
    public class Flight
    {
        [StringLength(1000)]
        public string? BookedSeats { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Origin name must be between 4 and 30 charaters long")]
        public string Origin { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Destination name must be between 4 and 30 charaters long")]
        public string Destination { get; set; }
        [Required]
        [DisplayName("Price Per Seat")]
        public double Price { get; set; } = 0;
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Airline name must be between 4 and 30 charaters long")]
        public string Airline { get; set; }
        [Key]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Flight ID must only contain 4 numbers")]
        [StringLength(4, MinimumLength = 4)]
        [DisplayName("Flight Number")]
        public string FlightId { get; set; }
        [Required]
        [DisplayName("Take Off Date")]
        public DateTime TakeOffDate { get; set; }
        [Required]
        [DisplayName("Landing Date")]
        public DateTime LandingDate { get; set; }
        [Required]
        public List<Seat> Seats { get; set; }
        [Required]
        [DisplayName("Number Of Seats")]
        public int NumberOfSeats  { get; set; }
        public List<Seat> setSeats(int n)
        {
            int x = 1, y = 1, z = 1, k = 1;
            var temp = new List<Seat>();
            for (int i = 0; i < n; i++)
            {
                if(i % 4 == 0)
                {
                    string b = (x).ToString();
                    x++;
                    string c = FlightId.ToString();
                    temp.Add(new Seat("A" + b + "-" + c));
                }
                else if((i + 2) % 4 == 0)
                {
                    string b = (y).ToString();
                    y++;
                    string c = FlightId.ToString();
                    temp.Add(new Seat("B" + b + "-" + c));
                }
                else if(i % 4 == 1)
                {
                    string b = (z).ToString();
                    z++;
                    string c = FlightId.ToString();
                    temp.Add(new Seat("C" + b + "-" + c));
                }
                else if ((i + 2) % 4 == 1)
                {
                    string b = (k).ToString();
                    k++;
                    string c = FlightId.ToString();
                    temp.Add(new Seat("D" + b + "-" + c));
                }
            }
            return temp;
        }
    }
}