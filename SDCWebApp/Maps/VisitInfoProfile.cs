using AutoMapper;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps
{
    public class VisitInfoProfile : Profile
    {
        public VisitInfoProfile()
        {
            CreateMap<VisitInfo, VisitInfoDto>()
                .ForMember(dest => dest.CreatedAt, options => options.AddTransform(datetime => datetime.Truncate(TimeSpan.FromSeconds(1))))
                .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime != null ? datetime.Truncate(TimeSpan.FromSeconds(1)) : null))
                .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime.Equals(DateTime.MinValue) ? null : datetime));

            CreateMap<VisitInfoDto, VisitInfo>()
                .ForMember(dest => dest.ConcurrencyToken, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.Ignore())
                .ForMember(dest => dest.UpdatedAt, options => options.Ignore());
        }
    }
}
