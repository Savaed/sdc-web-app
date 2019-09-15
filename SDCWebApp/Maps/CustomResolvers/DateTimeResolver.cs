using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;

namespace SDCWebApp.Maps.CustomResolvers
{
    public class DateTimeResolver : IValueResolver<RefreshToken, RefreshTokenDto, DateTime>
    {
        public DateTime Resolve(RefreshToken source, RefreshTokenDto destination, DateTime destMember, ResolutionContext context)
        {
            return DateTimeOffset.FromUnixTimeSeconds(source.ExpiryIn).DateTime;
        }
    }
}
