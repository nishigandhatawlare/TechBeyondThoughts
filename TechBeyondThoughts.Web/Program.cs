using Microsoft.AspNetCore.Authentication.Cookies;
using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Service;
using TechBeyondThoughts.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ITechService, TechService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<INewsService, NewsService>();
builder.Services.AddHttpClient<IBookService, BookService>();


SD.TechAPIBase = builder.Configuration["ServiceUrls:TechAPI"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.ContactAPIBase = builder.Configuration["ServiceUrls:ContactAPI"];
SD.BookAPIBase = builder.Configuration["ServiceUrls:BookAPI"];


builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITechService, TechService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddScoped<EmailService>(provider =>
    new EmailService(
        smtpHost: emailSettings.SmtpHost, // Use the updated property name
        smtpPort: emailSettings.SmtpPort,
        smtpUsername: emailSettings.SmtpUsername,
        smtpPassword: emailSettings.SmtpPassword,
        adminEmail: emailSettings.AdminEmail,
        enableSSL: emailSettings.EnableSSL,
        useDefaultCredentials: emailSettings.UseDefaultCredentials,
        isBodyHtml: emailSettings.IsBodyHTML // Use the updated property name
    )
);
var app = builder.Build();
  
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=LandingPage}/{id?}");

app.Run();
