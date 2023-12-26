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
                int pageSize = 4; 

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
                TempData["error"] = "An unexpected error occurred while processing your request.";
                   return View();
            }
        }

        public async Task<IActionResult> CreateBook(BookDataDto model)
        {
            if (ModelState.IsValid)
            {

                ResponceDto? responce = await _bookService.CreateBookAsync(model);

                if (responce != null && responce.IsSuccess)
                {
                    TempData["success"] = "Book Created Successfully!";
                    return RedirectToAction(nameof(BookIndex));
                }
                else
                {
                    TempData["error"] = responce?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> BookDetails(int id)
        {
            BookDataDto? model = new();
            ResponceDto? responce = await _bookService.GetBooksByIdAsync(id);

            if (responce != null && responce.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<BookDataDto>(Convert.ToString(responce.Result));
            }
            else
            {
                TempData["error"] = responce?.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            ResponceDto? responce = await _bookService.GetBooksByIdAsync(bookId);

            if (responce != null && responce.IsSuccess)
            {
                BookDataDto? model = JsonConvert.DeserializeObject<BookDataDto>(Convert.ToString(responce.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = responce?.Message;
            }
            return NotFound();
        }
        [HttpPost, ActionName("DeleteBook")]
        public async Task<IActionResult> DeleteConfirmed(int bookId)
        {
            try
            {
                ResponceDto? responce = await _bookService.DeleteBookAsync(bookId);

                if (responce != null && responce.IsSuccess)
                {
                    TempData["success"] = "Book Deleted Successfully!";

                    // Redirect to TechIndex upon successful deletion
                    return RedirectToAction(nameof(BookIndex));
                }
                else
                {
                    TempData["error"] = responce?.Message;
                }

                // If the deletion was not successful, redirect to a suitable action
                return RedirectToAction(nameof(BookIndex));
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes

                ModelState.AddModelError(string.Empty, "An error occurred while deleting the technology. Please try again.");
                // Redirect to a suitable action
                return RedirectToAction(nameof(BookIndex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int bookId)
        {
            ResponceDto? responce = await _bookService.GetBooksByIdAsync(bookId);

            if (responce != null && responce.IsSuccess)
            {
                BookDataDto? model = JsonConvert.DeserializeObject<BookDataDto>(Convert.ToString(responce.Result));

                // Set the Id property before passing it to the view
                model.BookId = bookId;

                return View(model);
            }
            else
            {
                TempData["error"] = responce?.Message;
                return NotFound();
            }
        }

        [HttpPost, ActionName("EditBook")]
        public async Task<IActionResult> EditConfirmed(BookDataDto bookData)
        {
            try
            {
                // Assuming you have access to ITechService through dependency injection
                ResponceDto? response = await _bookService.UpdateBookAsync(bookData.BookId, bookData);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book Updated Successfully!";

                    // Redirect to TechIndex upon successful update
                    return RedirectToAction(nameof(BookIndex));

                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                ModelState.AddModelError(string.Empty, "An error occurred while updating the technology. Please try again.");

                // Return to the edit view with the provided techData object
                return View("EditBook", bookData);
            }

            // If the update was not successful, return to the edit view with the provided techData object
            return View("EditBook", bookData);
        }

        public async Task<IActionResult> SearchBook(string keyword, string searchType)
        {
            List<BookDataDto> models = null;
            ResponceDto response = null;

            try
            {
                if (string.IsNullOrEmpty(searchType) || !IsValidSearchType(searchType))
                {
                    ModelState.AddModelError("searchType", "Invalid search type.");
                    return RedirectToAction("Index"); // Redirect to a default action or handle accordingly
                }

                switch (searchType.ToLower())
                {
                    case "category":
                        response = await _bookService.GetBooksByCategoryAsync(keyword);
                        break;

                    case "author":
                        response = await _bookService.GetBooksByAuthorAsync(keyword);
                        break;

                    case "title":
                        response = await _bookService.GetBooksByTitleAsync(keyword);
                        break;
                }

                if (response != null && response.IsSuccess)
                {
                    models = JsonConvert.DeserializeObject<List<BookDataDto>>(Convert.ToString(response.Result));
                }
                else
                {
                    TempData["error"] = response?.Message ?? "An error occurred while processing the search.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception details

                TempData["error"] = "An error occurred while processing the search.";
            }

            return View(models);
        }
        
        public async Task<IActionResult> DownloadBook(int id)
        {
            try
            {
                ResponceDto? downloadResponse = await _bookService.DownloadBookAsync(id);

                if (downloadResponse != null && downloadResponse.IsSuccess)
                {
                    // Assuming the downloaded content is in the response result
                    byte[] fileContents = downloadResponse.Result as byte[];
                    string fileName = "Book.pdf"; // You can customize the file name based on your API response

                    return File(fileContents, "application/pdf", fileName);
                }
                else
                {
                    TempData["error"] = downloadResponse?.Message;
                    return RedirectToAction("BookDetails"); // Redirect to a meaningful page or action
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("BookDetails");
            }
        }

        private bool IsValidSearchType(string searchType)
        {
            // Add any additional validation logic as needed 
            return searchType.ToLower() == "category" || searchType.ToLower() == "author" || searchType.ToLower() == "title";
        }
    }
}
