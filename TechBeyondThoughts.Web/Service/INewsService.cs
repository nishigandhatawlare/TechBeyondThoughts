using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
    public interface INewsService
    {
        Task<NewsApiResponse> GetTechnologyNewsAsync();

    }
}
