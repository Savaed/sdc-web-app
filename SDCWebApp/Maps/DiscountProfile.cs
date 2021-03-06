﻿using AutoMapper;
using SDCWebApp.Helpers.Extensions;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountDto>()
               .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime != null ? datetime : null))
               .ForMember(dest => dest.UpdatedAt, options => options.AddTransform(datetime => datetime.Equals(DateTime.MinValue) ? null : datetime));

            CreateMap<DiscountDto, Discount>()
               .ForMember(dest => dest.ConcurrencyToken, options => options.Ignore())
               .ForMember(dest => dest.CreatedAt, options => options.Ignore())
               .ForMember(dest => dest.UpdatedAt, options => options.Ignore());
        }
    }
}
