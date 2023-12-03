namespace TechBeyondThoughts.Services.AuthAPI.Models.Dto
{
	public class LoginResponceDto
	{
		public UserDto User { get; set; }
		public string Token { get; set; }
	}
}
