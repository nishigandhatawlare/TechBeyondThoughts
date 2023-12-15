using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Services.ContactAPI.Models.Dto
{
    public class ContactFormModelDto
    {

        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

    }
}
