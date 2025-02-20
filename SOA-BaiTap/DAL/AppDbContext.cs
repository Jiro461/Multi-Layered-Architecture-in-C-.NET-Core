using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.DAL
{
    public class AppDbContext : DbContext   
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MovieSeriesTag> MovieSeriesTags { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) :
       base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập quan hệ N-N giữa Movie và Tag
            modelBuilder.Entity<MovieSeriesTag>()
                .HasKey(mt => new { mt.MovieSeriesId, mt.TagId });

            modelBuilder.Entity<MovieSeriesTag>()
                .HasOne(mt => mt.Movie)
                .WithMany(m => m.MovieSeriesTags)
                .HasForeignKey(mt => mt.MovieSeriesId);

            modelBuilder.Entity<MovieSeriesTag>()
                .HasOne(mt => mt.Tag)
                .WithMany(t => t.MovieSeriesTags)
                .HasForeignKey(mt => mt.TagId);

            // Thiết lập quan hệ 1-N giữa Movie và Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            // Thiết lập quan hệ 1-N giữa Movie và Rating
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Rating>()
                .Property(r => r.Value)
                .HasPrecision(10, 4); // 10 chữ số tổng cộng, 4 chữ số thập phân

            
        }

    }
}
