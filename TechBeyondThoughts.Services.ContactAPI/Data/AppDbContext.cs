using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using TechBeyondThoughts.Services.ContactAPI.Models;
namespace TechBeyondThoughts.Services.ContactAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContactFormModel> ContactForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactFormModel>()
                .HasKey(c => c.ContactId); // Assuming you have an Id property in ContactFormModel

            // You can customize additional configurations here if needed
        }
    }
}
