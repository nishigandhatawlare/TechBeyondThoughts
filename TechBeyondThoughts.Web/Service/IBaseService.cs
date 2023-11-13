using TechBeyondThoughts.Web.Models;

namespace TechBeyondThoughts.Web.Service
{
    public interface IBaseService
    {
        Task<ResponceDto?> SendAsync(RequestDto requestDto);

    }
}
