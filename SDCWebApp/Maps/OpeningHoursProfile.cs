using AutoMapper;

using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Maps
{
    public class OpeningHoursProfile : Profile
    {
        public OpeningHoursProfile()
        {
            CreateMap<OpeningHours, OpeningHoursDto>();
            CreateMap<OpeningHoursDto, OpeningHours>();
        }
    }
}
