using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Maps
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountDto>();

            CreateMap<DiscountDto, Discount>()
                .ForMember(x => x.ConcurrencyToken, options => options.Ignore())
                .ForMember(x => x.CreatedAt, options => options.Ignore())
                .ForMember(x => x.UpdatedAt, options => options.Ignore());
        }
    }
}
