using Microsoft.AspNetCore.Mvc;
using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Service;

namespace TechBeyondThoughts.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
			_authService = authService;

		}
		[HttpGet]
        public IActionResult Login()
		{
			LoginRequestDto loginRequestDto = new();
			return View(loginRequestDto);
		}
		[HttpGet]

		public IActionResult Register()
		{
			return View();
		}
        public IActionResult Logout()
        {
            return View();
        }
    }
}
