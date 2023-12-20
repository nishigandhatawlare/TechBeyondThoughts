using Microsoft.EntityFrameworkCore;
using TechBeyondThoughts.Services.EBookAPI.Models;

namespace TechBeyondThoughts.Services.EBookAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BookData> Bookstacks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<DownloadHistory> DownloadHistory { get; set; }
        public DbSet<BookPreview> BookPreviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships here
            modelBuilder.Entity<BookData>()
                .HasMany(b => b.Ratings)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookData>()
                .HasMany(b => b.DownloadHistory)
                .WithOne(d => d.Book)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookData>()
                .HasOne(b => b.BookPreview)  // One-to-one relationship
                .WithOne(p => p.Book)
                .HasForeignKey<BookPreview>(p => p.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookData>().HasData(
        new BookData
        {
            BookId = 1,
            Title = "Book 1",
            Author = "Author 1",
            Category = "Fiction",
            ImageUrl = "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D",
            Format = "PDF",
            Published = DateTime.Now,
            Downloads = 100,
            Pages = 200,
            Description = "Description 1",
            Rating = 4.5,
            FileName = "book1.pdf",   // Set an example file name
            FilePath = "/storage/book1.pdf"  // Set an example file path or URL
        });

            // Seed Ratings
            modelBuilder.Entity<Rating>().HasData(
                new Rating
                {
                    UserId = 1,
                    Value = 4.0,
                    BookId = 1 // Reference to the seeded BookData
                });

            // Seed DownloadHistory
            modelBuilder.Entity<DownloadHistory>().HasData(
                new DownloadHistory
                {
                    UserId = 1,
                    DownloadDateTime = DateTime.Now,
                    BookId = 1 // Reference to the seeded BookData
                });

            // Seed BookPreview
            modelBuilder.Entity<BookPreview>().HasData(
                new BookPreview
                {
                    BookId = 1,
                    PagesToPreview = 5
                });
        }
    }
}
