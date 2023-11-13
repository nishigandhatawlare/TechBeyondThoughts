using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
    public interface ITechService
    {
        Task<ResponceDto?> GetTechAsync(string title);
        Task<ResponceDto?> GetAllTechAsync();
        Task<ResponceDto?> GetTechByIdAsync(int id);
        Task<ResponceDto?> CreateTechAsync(TechDataDto techDataDto);
        Task<ResponceDto?> UpdateTechAsync(TechDataDto techDataDto);
        Task<ResponceDto?> DeleteTechAsync(int id);
    }
}
