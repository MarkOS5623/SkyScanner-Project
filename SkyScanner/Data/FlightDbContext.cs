using Microsoft.EntityFrameworkCore;
using SkyScanner.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace SkyScanner.Data
{
    public class FlightDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>() //sets up a one to many relationship between flights and seats
            .HasOne(c => c.Flight).WithMany(c => c.Seats)
            .HasForeignKey(c => c.Flight_num);

           /* modelBuilder.Entity<Flight>()
           .Property(e => e.BookedSeats)
           .HasConversion(
               v => string.Join(',', v),
               v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));*/
        }
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
    }
}