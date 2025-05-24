using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VietTravelBE.Infrastructure.Data.Entities;

namespace VietTravelBE.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TourStartDate> TourStartDates { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<TourSchedule> TourSchedule { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<TourFavorite> TourFavorites { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Quan hệ 1-n : Region - City 
            modelBuilder.Entity<Region>()
                .HasMany(r => r.Cities)
                .WithOne(c => c.Region)
                .HasForeignKey(c => c.RegionId)
                .OnDelete(DeleteBehavior.NoAction);
            // Quan hệ 1-n: City - Hotel
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.City)
                .WithMany(c => c.Hotels)
                .HasForeignKey(h => h.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: City - Tour
            modelBuilder.Entity<Tour>()
                .HasOne(t => t.City)
                .WithMany(c => c.Tours)
                .HasForeignKey(t => t.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: Hotel - Room
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: User -Booking
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasOne(b => b.Hotel)
                      .WithMany(h => h.Bookings)
                      .HasForeignKey(b => b.HotelId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Tour)
                      .WithMany(t => t.Bookings)
                      .HasForeignKey(b => b.TourId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
                  
            // Quan hệ 1-n: Booking - Payment
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId);

            // Quan hệ 1-n: Tour - TourSchedule
            modelBuilder.Entity<TourSchedule>()
                .HasOne(ts => ts.Tour)
                .WithMany(t => t.TourSchedules)
                .HasForeignKey(ts => ts.TourId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: Tour - TourStartDate
            modelBuilder.Entity<TourStartDate>()
                .HasOne(ts => ts.Tour)
                .WithMany(t => t.TourStartDates)
                .HasForeignKey(ts => ts.TourId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: TourStartDate - Bookings
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TourStartDate)
                .WithMany(ts => ts.Bookings)
                .HasForeignKey(b => b.TourStartDateId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: User - Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: Tour - Review (có thể null)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Tour)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ 1-n: Hotel - Review (có thể null)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.NoAction);

            // Quan hệ n-n User - TourFavorite
            //modelBuilder.Entity<TourFavorite>()
            // .HasKey(tf => new { tf.UserId, tf.TourId }); // Định nghĩa khóa chính (UserId + TourId)

            modelBuilder.Entity<TourFavorite>()
            .HasKey(tf => tf.Id); // Định nghĩa Id là khóa chính

            modelBuilder.Entity<TourFavorite>()
                .Property(tf => tf.Id)
                .ValueGeneratedOnAdd(); // Id tự động tăng

            // Đảm bảo cặp UserId, TourId là unique để tránh trùng lặp
            modelBuilder.Entity<TourFavorite>()
                .HasIndex(tf => new { tf.UserId, tf.TourId })
                .IsUnique();

            modelBuilder.Entity<TourFavorite>()
                .HasOne(tf => tf.User)
                .WithMany(u => u.TourFavorites)
                .HasForeignKey(tf => tf.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Xóa User sẽ xóa luôn TourFavorites của User đó

            modelBuilder.Entity<TourFavorite>()
                .HasOne(tf => tf.Tour)
                .WithMany(t => t.TourFavorites)
                .HasForeignKey(tf => tf.TourId)
                .OnDelete(DeleteBehavior.NoAction); // Xóa Tour sẽ xóa luôn các User yêu thích Tour đó

            modelBuilder.Entity<Image>()
                .Property(i => i.ImageType)
                .IsRequired();

            modelBuilder.Entity<Image>()
                .HasIndex(i => new { i.EntityId, i.ImageType });

            modelBuilder.Entity<Image>()
                .Property(i => i.ImageType)
                .HasConversion<string>();
        }
    }
}
