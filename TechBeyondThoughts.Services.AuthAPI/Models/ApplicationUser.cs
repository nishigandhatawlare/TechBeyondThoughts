﻿using Microsoft.AspNetCore.Identity;

namespace TechBeyondThoughts.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
