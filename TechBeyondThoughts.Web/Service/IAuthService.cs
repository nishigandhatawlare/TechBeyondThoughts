using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
	public interface IAuthService
	{
		Task<ResponceDto> LoginAsync(LoginRequestDto loginRequestDto);
		Task<ResponceDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
		Task<ResponceDto> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);

	}
}
