using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Web.Models
{
	public class LoginRequestDto
	{
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }
	}
}
