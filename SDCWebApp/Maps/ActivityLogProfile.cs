using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Maps
{
    public class ActivityLogProfile : Profile
    {
        public ActivityLogProfile()
        {
            CreateMap<ActivityLog, ActivityLogDto>();
            CreateMap<ActivityLogDto, ActivityLog>();
        }
    }
}
