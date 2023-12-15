using AutoMapper;
using TechBeyondThoughts.Services.ContactAPI.Models;
using TechBeyondThoughts.Services.ContactAPI.Models.Dto;


namespace TechBeyondThoughts.Services.ContactAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ContactFormModelDto, ContactFormModel>();
            });

            return mappingConfig;
        }
    }
}
