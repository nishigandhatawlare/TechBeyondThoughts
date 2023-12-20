using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Web.Models
{
    public class BookDataDto
    {

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Format { get; set; }
        public DateTime Published { get; set; }
        public int Downloads { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public string FileName { get; set; }  // Store file name
        public string FilePath { get; set; }  // Store file path or URL

    }
}
