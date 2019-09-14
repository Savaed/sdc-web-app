using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;

namespace SDCWebApp.Maps
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshToken>();
        }
    }
}
