using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Services.EBookAPI.Models
{
    public class BookPreview
    {
        [Key]
        public int BookId { get; set; }
        public int PagesToPreview { get; set; }

        // Navigation property
        public BookData Book { get; set; }
    }
}
