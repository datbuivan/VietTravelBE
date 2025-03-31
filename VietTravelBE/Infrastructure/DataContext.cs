using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<City> City { get; set; }
        public DbSet<Evaluate> Evaluate { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TimePackage> TimePackage { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<TourPackage> TourPackage { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Room> Room { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Tours)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId)
                .IsRequired(false);
            modelBuilder.Entity<City>()
                .HasMany(e => e.Hotels)
                .WithOne(e => e.City)
                .HasForeignKey(e => e.CityId)
                .IsRequired(false); 
            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.Evaluates)
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
            modelBuilder.Entity<Tour>()
                .HasMany(e => e.Schedules)
                .WithOne(e => e.Tour)
                .HasForeignKey(e => e.TourId)
                .IsRequired(false);
            modelBuilder.Entity<Tour>()
                .HasMany(e => e.TourPackages)
                .WithOne(e => e.Tour)
                .HasForeignKey(e => e.TourId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
            modelBuilder.Entity<Hotel>()
                .HasMany(e => e.TourPackages)
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
            modelBuilder.Entity<Hotel>()
                .HasMany(e =>e.Rooms)
                .WithOne(e =>e.Hotel)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
            modelBuilder.Entity<TourPackage>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.TourPackage)
                .HasForeignKey(e => e.TourPackageId)
                .IsRequired(false);
            modelBuilder.Entity<TimePackage>()
                .HasMany(e => e.TourPackages)
                .WithOne(e => e.TimePackage)
                .HasForeignKey(e => e.TimePackageId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Evaluates)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);
            modelBuilder.Entity<User>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);
            modelBuilder.Entity<Schedule>()
                .HasMany(e => e.ScheduleTourPackages)
                .WithOne(e => e.Schedule)
                .HasForeignKey(e => e.ScheduleId)
                .IsRequired(false);
        }
    }
}
