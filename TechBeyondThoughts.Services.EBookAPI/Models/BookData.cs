using System.ComponentModel.DataAnnotations;
using TechBeyondThoughts.Services.EBookAPI.Models.Dto;

namespace TechBeyondThoughts.Services.EBookAPI.Models
{
    public class BookData
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; } // URL to an image representing the book

        public string Format { get; set; }
        public DateTime Published { get; set; }
        public int Downloads { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string FileName { get; set; }  // Store file name
        public string FilePath { get; set; }  // Store file path or URL

        // Navigation properties
        public List<Rating> Ratings { get; set; }
        public List<DownloadHistory> DownloadHistory { get; set; }
        public BookPreview BookPreview { get; set; }

    }
}
