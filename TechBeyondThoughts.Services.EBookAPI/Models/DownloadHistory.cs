using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Services.EBookAPI.Models
{
    public class DownloadHistory
    {
        public int UserId { get; set; }
        [Key]
        public int BookId { get; set; }
        public DateTime DownloadDateTime { get; set; }

        // Navigation property
        public BookData Book { get; set; }
    }
}
