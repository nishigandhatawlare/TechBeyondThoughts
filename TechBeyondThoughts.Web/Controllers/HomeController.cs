using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Service;

namespace TechBeyondThoughts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITechService _techService;
        private readonly EmailService _emailService;
        private readonly INewsService _newsService;



        public HomeController(ITechService techService, EmailService emailService, INewsService newsService)
        {
            _techService = techService;
            _emailService = emailService;
            _newsService = newsService;
        }
        public IActionResult LandingPage()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                // Set the page size (you can adjust this based on your requirements)
                int pageSize = 4;

                // Get technology news from the news service
                var newsResponse = await _newsService.GetTechnologyNewsAsync();

                if (newsResponse != null && newsResponse.Articles != null)
                {
                    // Paginate the list
                    var paginatedList = PaginatedList<Article>.Create(newsResponse.Articles, page ?? 1, pageSize);

                    return View(paginatedList);
                }
                else
                {
                    TempData["error"] = "Failed to retrieve technology news.";
                    return View();
                }
            }
            catch (HttpRequestException ex)
            {
                TempData["error"] = "Failed to connect to the news service.";
                return View();
            }
            catch (Exception ex)
            {
                TempData["error"] = "An unexpected error occurred while retrieving technology news.";
                return View();
            }
        }




        [Authorize]
        public async Task<IActionResult> techDetails(int id)
        {
            TechDataDto? model = new();
            ResponceDto? responce = await _techService.GetTechByIdAsync(id);

            if (responce != null && responce.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<TechDataDto>(Convert.ToString(responce.Result));

            }
            else
            {
                TempData["error"] = responce?.Message;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string message)
        {
            try
            {
                // Validate and process the form data (save to database, etc.)

                // Send email to admin
                _emailService.SendEmail(name, email, message);

                // Set success message
                TempData["success"] = "Your message has been sent successfully!";
            }
            catch (Exception ex)
            {
               
                // Set error message for demonstration purposes
                TempData["error"] = "An error occurred while sending the message. Please try again.";
            }

            // Redirect or return a response
            return RedirectToAction("Index");
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
		{
			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}