using AutoMapper;
using System;

using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Maps.CustomResolvers;

namespace SDCWebApp.Maps
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(x => x.CreatedAt, options => options.AddTransform(d => d.Truncate(TimeSpan.FromSeconds(1))))
                .ForMember(x => x.Links, options => options.MapFrom<TicketLinksResolver>()).ReverseMap();

            CreateMap<TicketDto, Ticket>()
                .ForMember(x => x.ConcurrencyToken, options => options.Ignore())
                .ForMember(x => x.UpdatedAt, options => options.Ignore())
                .ForMember(x => x.CreatedAt, options => options.Ignore());
        }
    }
}
