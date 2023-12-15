using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Services.ContactAPI.Models
{
    public class ContactFormModel
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
