using Microsoft.AspNetCore.Mvc;
using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
    public interface IBookService
    {
        Task<ResponceDto?> GetBooksAsync();
        Task<ResponceDto?> GetBooksByIdAsync(int id);
        Task<ResponceDto?> GetBooksByCategoryAsync(string category);
        Task<ResponceDto?> GetBooksByAuthorAsync(string author);
        Task<ResponceDto?> GetBooksByRatingAsync(double minRating, double maxRating);
        Task<ResponceDto?> GetDownloadHistoryAsync(int bookId);
        Task<ResponceDto?> GetBooksByTitleAsync(string name);
        Task<ResponceDto?> CreateBookAsync(BookDataDto bookDataDto);
        Task<ResponceDto?> UpdateBookAsync(int id, BookDataDto bookDataDto);
        Task<ResponceDto?> DeleteBookAsync(int id);
        Task<ResponceDto?> PostRatingAsync(int bookId, RatingDto ratingDto);
        Task<ResponceDto?> GetRatingsAsync(int bookId);
        Task<ResponceDto?> GetBookPreviewAsync(int bookId);
        Task<ResponceDto?> DownloadBookAsync(int bookId);
    }
}
