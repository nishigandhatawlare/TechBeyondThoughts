using AutoMapper;
using System;
using TechBeyondThoughts.Services.EBookAPI.Models.Dto;
using TechBeyondThoughts.Services.EBookAPI.Models;

namespace TechBeyondThoughts.Services.EBookAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Mapping for BookData
                config.CreateMap<BookDataDto, BookData>()
                     .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
    .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.FilePath));
                config.CreateMap<BookData, BookDataDto>();

                // Mapping for Rating
                config.CreateMap<RatingDto, Rating>();
                config.CreateMap<Rating, RatingDto>();

                // Mapping for DownloadHistory
                config.CreateMap<DownloadHistoryDto, DownloadHistory>();
                config.CreateMap<DownloadHistory, DownloadHistoryDto>();

                // Mapping for BookPreview
                config.CreateMap<BookPreviewDto, BookPreview>();
                config.CreateMap<BookPreview, BookPreviewDto>();

                // Adjust DateTime to DateTimeOffset
                config.CreateMap<DateTime, DateTimeOffset>().ConvertUsing(d => new DateTimeOffset(d));
                config.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(d => d.DateTime);
            });

            return mappingConfig;
        }
    }
}
