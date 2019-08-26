using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Dtos.Maps
{
    public class SighseeingTariffToDtoProfile : Profile
    {
        public SighseeingTariffToDtoProfile()
        {
            CreateMap<SightseeingTariff, SighseeingTariffDto>();
        }
    }
}
