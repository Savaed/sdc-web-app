using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDCWebApp.Helpers.Extensions;

namespace SDCWebApp.Maps
{
    public class TicketTariffProfile : Profile
    {
        public TicketTariffProfile()
        {
            CreateMap<TicketTariff, TicketTariffDto>()
                .ForMember(x => x.CreatedAt, options => options.AddTransform(d => d.Truncate(TimeSpan.FromSeconds(1))));

            CreateMap<TicketTariffDto, TicketTariff>()
                .ForMember(x => x.ConcurrencyToken, options => options.Ignore())
                .ForMember(x => x.UpdatedAt, options => options.Ignore())
                .ForMember(x => x.CreatedAt, options => options.Ignore());
        }
    }
}