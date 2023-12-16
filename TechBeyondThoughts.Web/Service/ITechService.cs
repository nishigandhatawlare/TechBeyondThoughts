using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
    public interface ITechService
    {
        Task<ResponceDto?> GetTechAsync(string title);
        Task<ResponceDto?> GetAllTechAsync();
        Task<ResponceDto?> GetTechByIdAsync(int techId);
        Task<ResponceDto?> CreateTechAsync(TechDataDto techDataDto);
        Task<ResponceDto?> UpdateTechAsync(int techId, TechDataDto techDataDto);
        Task<ResponceDto?> DeleteTechAsync(int id);
        Task<ResponceDto?> SearchTechByNameAsync(string keyword);

    }
}
