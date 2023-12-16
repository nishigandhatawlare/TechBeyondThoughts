using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Web.Models
{
    public class TechDataDto
    {
       
        public int Id { get; set; } // Unique identifier for each technology
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; } // Title of the technology (e.g., "Artificial Intelligence")
        [Required(ErrorMessage = "Description is required")]

        public string? Description { get; set; } // Brief description of the technology

        public string? Category { get; set; } // Category of the technology (e.g., "Machine Learning")

        public string? InventorName { get; set; } // Name of the inventor or creator of the technology
        public DateTime InventionDate { get; set; } // Date when the technology was invented
        public string? ImageUrl { get; set; } // URL to an image representing the technology

    }
}
