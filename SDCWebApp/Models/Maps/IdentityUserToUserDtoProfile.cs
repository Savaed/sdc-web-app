using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SDCWebApp.Models.ApiDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Models.Maps
{
    public class IdentityUserToUserDtoProfile : Profile
    {
        public IdentityUserToUserDtoProfile()
        {
            CreateMap<IdentityUser, UserDto>();
        }
    }
}
