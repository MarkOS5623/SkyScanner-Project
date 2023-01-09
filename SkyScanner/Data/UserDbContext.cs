using Microsoft.EntityFrameworkCore;
using SkyScanner.Models;

namespace SkyScanner.Data
{
    public class UserDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditCard>() //sets up a one to many relationship between User and Creditcard
            .HasOne(c => c.User).WithMany(c => c.CreditCards)
            .HasForeignKey(c => c.User_ID);
            modelBuilder.Entity<Booking>() //sets up a one to many relationship between User and Creditcard
            .HasOne(c => c.User).WithMany(c => c.Bookings)
            .HasForeignKey(c => c.User_ID);
        }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
