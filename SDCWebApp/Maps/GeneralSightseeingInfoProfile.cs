using AutoMapper;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps
{
    public class GeneralSightseeingInfoProfile : Profile
    {
        public GeneralSightseeingInfoProfile()
        {
            CreateMap<GeneralSightseeingInfo, SightseeingInfoDto>()
                .ForMember(x => x.CreatedAt, options => options.AddTransform(d => d.Truncate(TimeSpan.FromSeconds(1))))
                .ForMember(x => x.UpdatedAt, options => options.AddTransform(d => d != null ? d.Truncate(TimeSpan.FromSeconds(1)) : null))
                .ForMember(x => x.UpdatedAt, options => options.AddTransform(d => d.Equals(DateTime.MinValue) ? null : d));

            CreateMap<SightseeingInfoDto, GeneralSightseeingInfo>()
                .ForMember(x => x.ConcurrencyToken, options => options.Ignore())
                .ForMember(x => x.CreatedAt, options => options.Ignore())
                .ForMember(x => x.UpdatedAt, options => options.Ignore());
        }
    }
}
