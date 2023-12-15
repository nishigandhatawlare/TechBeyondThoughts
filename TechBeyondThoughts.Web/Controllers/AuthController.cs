using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Service;
using TechBeyondThoughts.Web.Utility;

namespace TechBeyondThoughts.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
			_authService = authService;
            _tokenProvider = tokenProvider;

        }
		[HttpGet]
        public IActionResult Login()
		{
			LoginRequestDto loginRequestDto = new();
			return View(loginRequestDto);
		}

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponceDto responceDto = await _authService.LoginAsync(obj);
            if (responceDto != null && responceDto.IsSuccess)
            {
                LoginResponceDto loginResponceDto = JsonConvert.DeserializeObject<LoginResponceDto>(Convert.ToString(responceDto.Result));
                await SignInUser(loginResponceDto);
                _tokenProvider.SetToken(loginResponceDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                TempData["error"] = responceDto.Message;
                return View(obj);
            }
           
        }

        [HttpGet]
		/*public IActionResult Register()
		{
			var roleList = new List<SelectListItem>()
			{
				new SelectListItem{ Text = SD.RoleAdmin, Value = SD.RoleCustomer},
                new SelectListItem{ Text = SD.RoleCustomer, Value = SD.RoleCustomer},

            };
			ViewBag.RoleList = roleList;
			return View();
		}*/

		public IActionResult Register()
		{
			var roleList = new List<SelectListItem>()
	        {
		      new SelectListItem{ Text = SD.RoleCustomer, Value = SD.RoleCustomer},
	        };
			ViewBag.RoleList = roleList;
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Register(RegistrationRequestDto obj)
		{
			ResponceDto result = await _authService.RegisterAsync(obj);
			ResponceDto assignRole;

			// Check if the selected role is not admin
			if (obj.Role != SD.RoleAdmin)
			{
				// Rest of your registration logic here
				if (result != null && result.IsSuccess)
				{
					if (string.IsNullOrEmpty(obj.Role))
					{
						obj.Role = SD.RoleCustomer;
					}

					assignRole = await _authService.AssignRoleAsync(obj);

					if (assignRole != null && assignRole.IsSuccess)
					{
						TempData["success"] = "Registration Successful!";
						return RedirectToAction(nameof(Login));
					}
					else
					{
						TempData["error"] = result.Message;
					}
				}
			}
			else
			{
				TempData["error"] = "Registration as admin is not allowed.";
			}

			// If the role is admin or an error occurred, reload the registration view
			var roleList = new List<SelectListItem>()
	{
		new SelectListItem{ Text = SD.RoleCustomer, Value = SD.RoleCustomer},
	};
			ViewBag.RoleList = roleList;
			return View(obj);
		}

		public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");
        }

        private async Task SignInUser(LoginResponceDto model) 
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(model.Token);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

                var principle = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
            }
            catch (Exception ex) { 
            
            }
        }
    }
}
