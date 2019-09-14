using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps
{
    public class SightseeingGroupProfile : Profile
    {
        public SightseeingGroupProfile()
        {
            CreateMap<SightseeingGroup, SightseeingGroupDto>()
               .ForMember(x => x.UpdatedAt, options => options.AddTransform(d => d.Equals(DateTime.MinValue) ? null : d));

            CreateMap<SightseeingGroupDto, SightseeingGroup>()
                .ForMember(x => x.ConcurrencyToken, options => options.Ignore())
                .ForMember(x => x.UpdatedAt, options => options.Ignore())
                .ForMember(x => x.CreatedAt, options => options.Ignore());
        }
    }
}
