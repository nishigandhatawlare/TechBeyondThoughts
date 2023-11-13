using TechBeyondThoughts.Web.Models;
using TechBeyondThoughts.Web.Utility;

namespace TechBeyondThoughts.Web.Service
{
    public class TechService : ITechService
    {
        private readonly IBaseService _baseService;
        public TechService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponceDto?> CreateTechAsync(TechDataDto techDataDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = techDataDto,
                Url = SD.TechAPIBase + "/api/tech"
            }
            );
        }
        public async Task<ResponceDto?> DeleteTechAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.TechAPIBase + "/api/tech/" + id
            }
                       );
        }
        public async Task<ResponceDto?> GetAllTechAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.TechAPIBase + "/api/tech"
            }
            );
        }
        public async Task<ResponceDto?> GetTechAsync(string title)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.TechAPIBase + "/api/tech/GetByName/" + title
            }
            );
        }
        public async Task<ResponceDto?> GetTechByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.TechAPIBase + "/api/tech/" + id
            }
           );
        }
        public async Task<ResponceDto?> UpdateTechAsync(TechDataDto techDataDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = techDataDto,
                Url = SD.TechAPIBase + "/api/coupon"
            }
                        );
        }
    }
}
