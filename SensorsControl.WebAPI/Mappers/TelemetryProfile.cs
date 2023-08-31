using AutoMapper;
using SensorsControl.Repositories.Entities;
using SensorsControl.Services.Models;
using SensorsControl.WebAPI.Dto;

namespace SensorsControl.WebAPI.Mappers
{
    public class TelemetryProfile : Profile
    {
        public TelemetryProfile()
        {
            CreateMap<TelemetryDto, TelemetryModel>()
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => ConvertToDateTime(src.Time)));
            CreateMap<UnitModel, UnitDto>()
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));
            CreateMap<TelemetryModel, TelemetryEntity>()
                .ForMember(dst => dst.Date, opt => opt.MapFrom(src => src.Time));
            CreateMap<DailyTelemetryEntity, UnitModel>()
                .ForMember(dst => dst.MaxIlluminance, opt => opt.MapFrom(src => src.DailyRecords.MaxBy(rc => rc.Illum)!.Illum));
        }

        private DateTime ConvertToDateTime(long val)
        {
            return DateTimeOffset.FromUnixTimeSeconds(val).UtcDateTime;
        }
    }
}
