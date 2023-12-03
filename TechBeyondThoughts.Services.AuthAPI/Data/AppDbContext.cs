﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechBeyondThoughts.Services.AuthAPI.Models;
using static Azure.Core.HttpHeader;
//using TechBeyondThoughts.Services.TechAPI.Models;
namespace TechBeyondThoughts.Services.AuthAPI.Data
{
    public class AppDbContext :  IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
