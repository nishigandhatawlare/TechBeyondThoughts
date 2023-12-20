using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Service;

namespace TechBeyondThoughts.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> BookIndex(int? page)
        {
            try
            {
                int pageSize = 3; // Set the desired page size

                // Retrieve success message from TempData

                ResponceDto? response = await _bookService.GetBooksAsync();

                if (response != null && response.IsSuccess)
                {
                    var bookList = JsonConvert.DeserializeObject<List<BookDataDto>>(Convert.ToString(response.Result));

                    var paginatedList = PaginatedList<BookDataDto>.Create(bookList, page ?? 1, pageSize);

                    return View(paginatedList);
                }
                else
                {
                    TempData["error"] = response?.Message ?? "An error occurred while retrieving books.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or perform other error handling tasks
                TempData["error"] = "An unexpected error occurred while processing your request.";
                // You might also want to log the exception for further investigation
                // Logger.LogError(ex, "An unexpected error occurred in BookIndex method.");

                return View();
            }
        }

    }
}
