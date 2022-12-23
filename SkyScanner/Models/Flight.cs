using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



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
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Flight ID must only contain 4 numbers")]
        [StringLength(4, MinimumLength = 4)]
        public string FlightId { get; set; }
        [Required]

        public DateTime DepartureDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public List<Seat> Seats { get; set; }
        [Required]

        public int NumberOfSeats  { get; set; }
        public List<Seat> setSeats(int n)
        {
            var temp = new List<Seat>();
            for (int i = 0; i < n; i++)
            {
                if(i < n / 2)
                {
                    string b = (i+1).ToString();
                    string c = FlightId.ToString();
                    temp.Add(new Seat(c+"-A"+b));
                }
                else
                {
                    string b = (i + 1 - 10).ToString();
                    string c = FlightId.ToString();
                    temp.Add(new Seat(c+"-B" + b));
                }
            }
            return temp;
        }
   
    }
}