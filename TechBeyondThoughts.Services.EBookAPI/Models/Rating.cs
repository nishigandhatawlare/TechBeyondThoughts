using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Services.EBookAPI.Models
{
    public class Rating
    {
        public int UserId { get; set; }
        [Key]
        public int BookId { get; set; }
        public double Value { get; set; }

        // Navigation property
        public BookData Book { get; set; }
    }
}
