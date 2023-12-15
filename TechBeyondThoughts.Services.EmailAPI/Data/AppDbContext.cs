using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using TechBeyondThoughts.Services.EmailAPI.Models;
namespace TechBeyondThoughts.Services.EmailAPI.Data
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<EmailLogger> EmailLoggers { get; set; }
        
    }
}
