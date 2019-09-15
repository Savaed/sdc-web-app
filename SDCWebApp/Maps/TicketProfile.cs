using AutoMapper;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Maps.CustomResolvers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.CreatedAt, options => options.AddTransform(datetime => datetime.Truncate(TimeSpan.FromSeconds(1))))
                .ForMember(dest => dest.Links, options => options.MapFrom<TicketLinksResolver>()).ReverseMap();

            CreateMap<TicketDto, Ticket>()
                .ForMember(dest => dest.ConcurrencyToken, options => options.Ignore())
                .ForMember(dest => dest.UpdatedAt, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.Ignore());
        }
    }
}
