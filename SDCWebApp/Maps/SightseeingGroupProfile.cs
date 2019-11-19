using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Helpers.Extensions;
using System;

namespace SDCWebApp.Maps
{
    public class SightseeingGroupProfile : Profile
    {
        public SightseeingGroupProfile()
        {
            CreateMap<SightseeingGroup, SightseeingGroupDto>()
                .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime != null ? datetime : null))
                .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime.Equals(DateTime.MinValue) ? null : datetime));

            CreateMap<SightseeingGroupDto, SightseeingGroup>()
                .ForMember(dest => dest.ConcurrencyToken, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.Ignore())
                .ForMember(dest => dest.UpdatedAt, options => options.Ignore());
        }
    }
}
