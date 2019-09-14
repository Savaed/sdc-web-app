using AutoMapper;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace SDCWebApp.Maps.CustomResolvers
{
    /// <summary>
    /// Resolves navigation properties from source <see cref="Ticket"/> to appropriate links in destination <see cref="TicketDto"/>.
    /// </summary>
    public class TicketLinksResolver : IValueResolver<Ticket, TicketDto, IEnumerable<ApiLink>>
    {
        private const string CustomerResourceName = "customers";
        private const string DiscountResourceName = "discounts";
        private const string TicketTariffResourceName = "ticket-tariffs";
        private const string SightseeingGroupResourceName = "groups";
        private const string GetMethod = "GET";


        public IEnumerable<ApiLink> Resolve(Ticket source, TicketDto destination, IEnumerable<ApiLink> destMember, ResolutionContext context)
        {
            var links = new ApiLink[]
            {
                new ApiLink(nameof(Customer), $"{CustomerResourceName}/{source.Customer.Id}", GetMethod),
                new ApiLink(nameof(Discount), $"{DiscountResourceName}/{source.Discount.Id}", GetMethod),
                new ApiLink(nameof(TicketTariff), $"{TicketTariffResourceName}/{source.Tariff.Id}", GetMethod),
                new ApiLink(nameof(SightseeingGroup), $"{SightseeingGroupResourceName}/{source.Group.Id}", GetMethod)
            };

            return links.AsEnumerable();
        }
    }
}
