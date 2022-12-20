using Microsoft.EntityFrameworkCore;
using SkyScanner.Models;

namespace SkyScanner.Data
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flights { get; set; }
    }
}