using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Utility;

namespace TechBeyondThoughts.Web.Service
{
    public class BookService : IBookService
    {
        private readonly IBaseService _baseService;

        public BookService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponceDto?> CreateBookAsync(BookDataDto bookDataDto) //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = bookDataDto,
                Url = SD.BookAPIBase + "/api/book"
            }
            );
        }

        public async Task<ResponceDto?> DeleteBookAsync(int id)    //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.BookAPIBase}/api/book/id:int?id={id}"
            }
            );
        }

        public async Task<ResponceDto?> DownloadBookAsync(int bookId)
        {
            return await _baseService.SendPdfAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/DownloadBook/{bookId}"
            });
        }

        /* public async Task<ResponceDto?> DownloadBookAsync(int bookId)
         {
             try
             {
                 var apiUrl = $"{SD.BookAPIBase}/api/book/DownloadBook/{bookId}";
                 var requestDto = new RequestDto { ApiType = SD.ApiType.GET, Url = apiUrl };
                 var downloadResponse = await _baseService.SendPdfAsync(requestDto);

                 return downloadResponse;
             }
             catch (Exception ex)
             {
                 var dto = new ResponceDto
                 {
                     Message = ex.Message.ToString(),
                     IsSuccess = false
                 };
                 return dto;
             }
         }
 */

       
        public async Task<ResponceDto?> GetBooksAsync()  //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BookAPIBase + "/api/book"
            }
            );
        }

        public async Task<ResponceDto?> GetBooksByAuthorAsync(string author)       //*****
        {

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/GetByAuthor/{author}"
            }
            );
        }

        public async Task<ResponceDto?> GetBooksByCategoryAsync(string category)    //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/GetByCategory/{category}"
            }
            );
        }

        public async Task<ResponceDto?> GetBooksByIdAsync(int id)      //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/id:int?id={id}"
            });
        }

        public async Task<ResponceDto?> GetBooksByRatingAsync(double minRating, double maxRating)   //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/GetByRating/{minRating}/{maxRating}"
            });
        }

        public async Task<ResponceDto?> GetBooksByTitleAsync(string name)   //*****
        {

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/GetByName/{name}"
            });
        }

        public async Task<ResponceDto?> GetDownloadHistoryAsync(int bookId)    //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/DownloadHistory/{bookId}"
            }
            );
        }

        public async Task<ResponceDto?> GetRatingsAsync(int bookId)    //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.BookAPIBase}/api/book/GetRatings/{bookId}"
            }
            );
        }

        public async Task<ResponceDto?> PostRatingAsync(int bookId, RatingDto ratingDto)  //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = ratingDto,
                Url = $"{SD.BookAPIBase}/api/book/PostRating/{bookId}"
            }
            );
        }

        public async Task<ResponceDto?> UpdateBookAsync(int id, BookDataDto bookDataDto)  //*****
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = bookDataDto,
                Url = $"{SD.BookAPIBase}/api/book/{id}"
            });
        }
    }
}
