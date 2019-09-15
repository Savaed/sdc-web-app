using AutoMapper;
using SDCWebApp.Maps.CustomResolvers;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Maps
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>()
                .ForMember(dest => dest.ExpiryIn, options => options.MapFrom<DateTimeResolver>()).ReverseMap();

            CreateMap<RefreshTokenDto, RefreshToken>();
        }
    }
}
