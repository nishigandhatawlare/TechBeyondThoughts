using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using TechBeyondThoughts.Services.TechAPI.Models;
namespace TechBeyondThoughts.Services.TechAPI.Data
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<TechData> Techstacks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TechData>().HasData(new TechData
            {
                Id = 1,
                Title = "Artificial Intelligence",
                Description = "A field of computer science that simulates human intelligence in machines.",
                Category = "Machine Learning",
                InventorName = "John McCarthy",
                InventionDate = new DateTime(1956, 8, 31),
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Ffree-photos-vectors%2Fhr-ai%2F62&psig=AOvVaw3vymYJOV0QJXmKzYhn1jCM&ust=1699509512867000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCKjS67jcs4IDFQAAAAAdAAAAABAJ"
            });

            modelBuilder.Entity<TechData>().HasData(new TechData
            {
                Id = 2,
                Title = "Blockchain",
                Description = "A distributed ledger technology for secure and transparent transactions.",
                Category = "Cryptocurrency",
                InventorName = "Satoshi Nakamoto",
                InventionDate = new DateTime(2008, 10, 31),
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.infoq.com%2Farticles%2Fblockchain-as-a-service-get-block%2F&psig=AOvVaw3jbqS1fPY4Bxv_HzE9cUwE&ust=1699509637212000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCMDmgfTcs4IDFQAAAAAdAAAAABAE"
            });
        }
    }
}
