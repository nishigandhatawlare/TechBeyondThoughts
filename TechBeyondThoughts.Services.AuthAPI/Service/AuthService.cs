using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechBeyondThoughts.Services.AuthAPI.Data;
using TechBeyondThoughts.Services.AuthAPI.Models;
using TechBeyondThoughts.Services.AuthAPI.Models.Dto;
using TechBeyondThoughts.Services.AuthAPI.Service.IService;

namespace TechBeyondThoughts.Services.AuthAPI.Service
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator ,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
			_db = db;
			_jwtTokenGenerator = jwtTokenGenerator;
			_userManager = userManager;
			_roleManager = roleManager;
        }

		public async Task<bool> AssignRole(string email, string roleName)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
			if (user != null) {
				if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
				{
					//create role if it does not exist
					_roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
				}
				await _userManager.AddToRoleAsync(user,roleName);
				return true;
			}
			return false;
		}

		public async Task<LoginResponceDto> Login(LoginRequestDto loginRequestDto)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(u=> u.UserName.ToLower() == loginRequestDto.Username.ToLower());
			bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
		    if ( user ==null || isValid == false)
			{
				return new LoginResponceDto() {User= null,Token="" };
			}
			//if user found , we need to Generate JWT Token
			var roles = await _userManager.GetRolesAsync(user);
			 var token =  _jwtTokenGenerator.GenerateToken(user, roles);

			UserDto userDto = new()
			{
				Email = user.Email,
				Id = user.Id,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber
			};

			LoginResponceDto loginResponceDto = new LoginResponceDto()
			{ 
				User = userDto,
				Token =token
			};
			return loginResponceDto;
		}

		public async Task<string> Register( RegistrationRequestDto registrationRequestDto)
		{
			ApplicationUser user = new()
			{
				UserName = registrationRequestDto.Email,
				Email = registrationRequestDto.Email,
				NormalizedEmail = registrationRequestDto.Email.ToUpper(),
				Name = registrationRequestDto.Name,
				PhoneNumber = registrationRequestDto.PhoneNumber
			};
			try
			{
				var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
				if (result.Succeeded)
				{
					var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
					UserDto userDto = new()
					{
						Email = userToReturn.Email,
						Id = userToReturn.Id,
						Name = userToReturn.Name,
						PhoneNumber = userToReturn.PhoneNumber
					};
					return "";
				}
				else {
					return result.Errors.FirstOrDefault().Description;
				}

			}
			catch (Exception ex){ 
			
			}
			return "";
		}
	}
}
