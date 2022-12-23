using Microsoft.EntityFrameworkCore;
using SkyScanner.Models;

namespace SkyScanner.Data
{
    public class SeatDbContext : DbContext
    {
        public SeatDbContext(DbContextOptions<SeatDbContext> options) : base(options)
        {
            
        }
        public DbSet<Seat> Seats { get; set; }
    }
}
