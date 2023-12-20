using System.ComponentModel.DataAnnotations;

namespace TechBeyondThoughts.Web.Models
{
	public class RegistrationRequestDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required.")]
    [Phone(ErrorMessage = "Invalid phone number.")]
	[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should contain exactly 10 digits")]

	public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required.")]

    [DataType(DataType.Password)]
    [Compare("ConfirmPassword", ErrorMessage = "Passward does not match")]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
    public string Role { get; set; }
}

}
