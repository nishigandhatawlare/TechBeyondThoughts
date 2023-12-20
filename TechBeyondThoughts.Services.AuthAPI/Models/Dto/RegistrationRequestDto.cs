using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Services.AuthAPI.Models.Dto
{
	public class RegistrationRequestDto
	{
		public string Email { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		[Compare("ConfirmPassword", ErrorMessage = "Passward does not match")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }

	}
}
