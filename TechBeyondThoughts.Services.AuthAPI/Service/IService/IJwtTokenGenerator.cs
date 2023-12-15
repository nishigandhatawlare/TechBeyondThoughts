using TechBeyondThoughts.Services.AuthAPI.Models;

namespace TechBeyondThoughts.Services.AuthAPI.Service.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);

	}
}
