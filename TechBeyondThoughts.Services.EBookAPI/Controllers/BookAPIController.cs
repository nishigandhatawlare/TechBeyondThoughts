using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pdfium.NET;
using Sieve.Services;
using TechBeyondThoughts.Services.EBookAPI.Data;
using TechBeyondThoughts.Services.EBookAPI.Models;
using TechBeyondThoughts.Services.EBookAPI.Models.Dto;

namespace TechBeyondThoughts.Services.EBookAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    [Authorize]   
    public class BookAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponceDto _responce;
        private IMapper _mapper;
		private readonly ISieveProcessor _sieveProcessor;

		public BookAPIController(AppDbContext db, ISieveProcessor sieveProcessor,  IMapper mapper) {
            _db = db;
            _mapper = mapper;
            _responce = new ResponceDto();
            _sieveProcessor = sieveProcessor;
        }

        [HttpGet]
        public ResponceDto GetBooks()
        {
            try
            {
                IEnumerable<BookData> objList = _db.Bookstacks.ToList();
                _responce.Result = _mapper.Map<IEnumerable<BookDataDto>>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;

        }

        [HttpGet]
        [Route("id:int")]
        public ResponceDto GetBooksById(int id)
        {
            try
            {
                BookData objList = _db.Bookstacks.First(u => u.BookId == id);
                _responce.Result = _mapper.Map<BookDataDto>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;

        }

        [HttpGet]
        [Route("GetByCategory/{category}")]
        public ResponceDto GetBooksByCategory(string category)
        {
            try
            {
                // Case-insensitive search for books with a category containing the specified substring
                List<BookData> objList = _db.Bookstacks
                    .Where(u => u.Category != null && u.Category.ToLower().Contains(category.ToLower()))
                    .ToList();

                if (objList == null || objList.Count == 0)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "No books found for the specified category.";
                }
                else
                {
                    _responce.IsSuccess = true;
                    _responce.Result = _mapper.Map<List<BookDataDto>>(objList);
                }
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }

            return _responce;
        }

        [HttpGet]
        [Route("GetByAuthor/{author}")]
        public ResponceDto GetBooksByAuthor(string author)
        {
            try
            {
                // Case-insensitive search for books with an author containing the specified substring
                List<BookData> objList = _db.Bookstacks
                    .Where(u => u.Author != null && u.Author.ToLower().Contains(author.ToLower()))
                    .ToList();

                if (objList == null || objList.Count == 0)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "No books found for the specified author.";
                }
                else
                {
                    _responce.IsSuccess = true;
                    _responce.Result = _mapper.Map<List<BookDataDto>>(objList);
                }
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }

            return _responce;
        }


        [HttpGet]
        [Route("GetByRating/{minRating}/{maxRating}")]
        public ResponceDto GetBooksByRating(double minRating, double maxRating)
        {
            try
            {
                List<BookData> objList = _db.Bookstacks.Where(u => u.Rating >= minRating && u.Rating <= maxRating).ToList();

                if (objList == null || objList.Count == 0)
                {
                    _responce.IsSuccess = false;
                }

                _responce.Result = _mapper.Map<List<BookDataDto>>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }

            return _responce;
        }

        [HttpGet]
        [Route("DownloadHistory/{bookId:int}")]
        public ResponceDto GetDownloadHistory(int bookId)
        {
            try
            {
                List<DownloadHistory> downloadHistoryList = _db.DownloadHistory.Where(d => d.BookId == bookId).ToList();

                _responce.Result = _mapper.Map<List<DownloadHistoryDto>>(downloadHistoryList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }

            return _responce;
        }   //pending


        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponceDto GetBooksByTitle(string name)
        {
            try
            {
                // Case-insensitive search for books with a title containing the specified substring
                List<BookData> objList = _db.Bookstacks
                    .Where(u => u.Title != null && u.Title.ToLower().Contains(name.ToLower()))
                    .ToList();

                if (objList == null || objList.Count == 0)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "No books found for the specified title.";
                }
                else
                {
                    _responce.IsSuccess = true;
                    _responce.Result = _mapper.Map<List<BookDataDto>>(objList);
                }
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }

            return _responce;
        }



        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponceDto CreateBook([FromBody] BookDataDto bookDataDto)
        {
            try
            {
                BookData objList = _mapper.Map<BookData>(bookDataDto);
                _db.Bookstacks.Add(objList);
                _db.SaveChanges();
                _responce.Result = _mapper.Map<BookDataDto>(objList);

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        [Route("{id:int}")]
        public ResponceDto UpdateBook(int id, [FromBody] BookDataDto bookDataDto)
        {
            try
            {
                BookData objList = _db.Bookstacks.FirstOrDefault(u => u.BookId == id);

                if (objList == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Resource not found";
                    return _responce;
                }

                _mapper.Map(bookDataDto, objList);

                _db.Bookstacks.Update(objList);
                _db.SaveChanges();

                _responce.Result = _mapper.Map<BookDataDto>(objList);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }




        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("id:int")]
        public ResponceDto DeleteBook(int id)
        {
            try
            {
                BookData objList = _db.Bookstacks.First(u => u.BookId == id);
                _db.Bookstacks.Remove(objList);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }

        //ratings related 

        [HttpPost]
        [Route("PostRating/{bookId:int}")]
        [Authorize] // Assuming only authenticated users can post ratings
        public ResponceDto PostRating(int bookId, [FromBody] RatingDto ratingDto)
        {
            _responce = new ResponceDto(); // Initialize the response object

            try
            {
                // Check if the book exists
                BookData book = _db.Bookstacks.FirstOrDefault(b => b.BookId == bookId);
                if (book == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Book not found";
                    return _responce;
                }

                // Map the RatingDto to Rating entity
                Rating rating = _mapper.Map<Rating>(ratingDto);
                rating.BookId = bookId;

                // Check if the entity is already being tracked
                var existingRating = _db.Ratings.Local.FirstOrDefault(r => r.UserId == ratingDto.UserId && r.BookId == bookId);

                if (existingRating != null)
                {
                    // Entity is already being tracked, update its values
                    _db.Entry(existingRating).CurrentValues.SetValues(rating);
                }
                else
                {
                    // Entity is not being tracked, attach or add it
                    _db.Ratings.Attach(rating);
                }

                _db.SaveChanges();

                _responce.Result = _mapper.Map<RatingDto>(rating);
                _responce.IsSuccess = true; // Set success flag
            }
            catch (DbUpdateException ex)
            {
                // Handle specific DbUpdateException
                _responce.Message = "Error updating the database.";
                // Log ex or handle as needed
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                _responce.Message = "An error occurred.";
                // Log ex or handle as needed
            }
            return _responce;
        }

        [HttpGet]
        [Route("GetRatings/{bookId:int}")]
        public ResponceDto GetRatings(int bookId)
        {
            try
            {
                // Check if the book exists
                BookData book = _db.Bookstacks.FirstOrDefault(b => b.BookId == bookId);
                if (book == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Book not found";
                    return _responce;
                }

                // Get ratings for the specified book
                List<Rating> ratings = _db.Ratings.Where(r => r.BookId == bookId).ToList();
                _responce.Result = _mapper.Map<List<RatingDto>>(ratings);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
            }
            return _responce;
        }
       
        //download a book 
        [HttpGet]
        [Route("DownloadBook/{bookId:int}")]
        public IActionResult DownloadBook(int bookId)
        {
            try
            {
                // Check if the book exists
                BookData book = _db.Bookstacks.FirstOrDefault(b => b.BookId == bookId);
                if (book == null)
                {
                    _responce.IsSuccess = false;
                    _responce.Message = "Book not found";
                    return NotFound(_responce);
                }

                // Example: Get the full file path
                string filePath = Path.Combine("D:\\TechBeyondThoughts\\files\\", book.FileName);

                // Read the file content
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Set the content type based on the file format (e.g., application/pdf for PDF)
                string contentType = "application/pdf"; // Adjust based on your file format

                // Return the file content as a file result
                return File(fileBytes, contentType, book.FileName);
            }
            catch (Exception ex)
            {
                _responce.IsSuccess = false;
                _responce.Message = ex.Message;
                return StatusCode(500, _responce);
            }
        }





    }
}
