using AutoMapper;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps
{
    public class TicketTariffProfile : Profile
    {
        public TicketTariffProfile()
        {
            CreateMap<TicketTariff, TicketTariffDto>()
                .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime != null ? datetime : null))
                .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime.Equals(DateTime.MinValue) ? null : datetime))
                .ForMember(dest => dest.VisitTariffId, options => options.MapFrom(src => src.VisitTariff.Id));

            CreateMap<TicketTariffDto, TicketTariff>()
                .ForMember(dest => dest.ConcurrencyToken, options => options.Ignore())
                .ForMember(dest => dest.UpdatedAt, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.Ignore());
        }
    }
}