using AutoMapper;
using TechBeyondThoughts.Services.TechAPI.Models;
using TechBeyondThoughts.Services.TechAPI.Models.Dto;

namespace TechBeyondThoughts.Services.TechAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TechDataDto, TechData>();
                config.CreateMap<TechData, TechDataDto>();
            });

            return mappingConfig;
        }
    }
}
