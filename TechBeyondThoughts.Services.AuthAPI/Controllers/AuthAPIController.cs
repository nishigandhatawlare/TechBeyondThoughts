using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechBeyondThoughts.Services.AuthAPI.Models.Dto;
using TechBeyondThoughts.Services.AuthAPI.Service.IService;

namespace TechBeyondThoughts.Services.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthAPIController : ControllerBase
	{

		private readonly IAuthService _authService;
		private readonly ResponceDto _responce;

        public AuthAPIController(IAuthService authService)
        {
			_authService = authService;
			_responce = new();
		}

        [HttpPost("register")]
		public async Task<IActionResult> Register(RegistrationRequestDto model) 
		{
			var errorMessage = await _authService.Register(model);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				_responce.IsSuccess = false;
				_responce.Message = errorMessage;
				return BadRequest(_responce);
			}
			return Ok(_responce);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequestDto model)
		{
			var loginResponce = await _authService.Login(model);
			if (loginResponce.User == null) {
				_responce.IsSuccess = false;
				_responce.Message = "Username or password is Incorrect!";
				return BadRequest(_responce);
			}
			_responce.Result = loginResponce;
			return Ok(_responce);

		}
		[HttpPost("AssignRole")]
		public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
		{
			var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper());
			if (!assignRoleSuccessful)
			{
				_responce.IsSuccess = false;
				_responce.Message = "Error Encountered!";
				return BadRequest(_responce);
			}
			return Ok(_responce);

		}

	}
}
