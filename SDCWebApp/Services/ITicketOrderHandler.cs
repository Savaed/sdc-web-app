using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ITicketOrderHandler
    {
        /// <summary>
        /// Gets the order ID associated with the customer and the tickets he ordered.
        /// </summary>
        string OrderId { get; }

        /// <summary>
        /// Gets customer associated with particular ticket order.
        /// </summary>
        Customer Customer { get; }

        /// <summary>
        /// Gets tickets associated with particular ticket order.
        /// </summary>
        IEnumerable<Ticket> Tickets { get; }

        /// <summary>
        /// Asynchronously creates <see cref="Models.Customer"/> customer and <see cref="Ticket"/> tickets for this particular order.
        /// Throws exception if argument is invalid or if cannot create ticket order properly.
        /// </summary>
        /// <param name="order">The order request data for which a ticket order will be created.</param>
        /// <exception cref="ArgumentNullException"><paramref name="order"/> is null.</exception>
        /// <exception cref="ArgumentException"><see cref="OrderRequestDto.Customer"/> or <see cref="OrderRequestDto.Tickets"/> is null.</exception>
        /// <exception cref="InternalDbServiceException">Cannot create ticket order properly.</exception>
        Task CreateOrderAsync(OrderRequestDto order);

        /// <summary>
        /// Asynchronously creates new ones or updates existent <see cref="SightseeingGroup"/> groups by adding ordered <see cref="Ticket"/> tickets to them.
        /// Throws exception if the group cannot be properly handled due to its invalid state or if any problems while processing order data in the database occurred.
        /// </summary>
        /// <exception cref="InvalidOperationException">The group cannot be properly handled.</exception>
        /// <exception cref="InternalDbServiceException">Encountered problems while processing order data in the database.</exception>
        Task HandleOrderAsync();

        /// <summary>
        /// Asynchronously retrieves <see cref="Ticket"/> from ticket order specified by <paramref name="orderId"/>.
        /// Throws exception if <paramref name="orderId"/> has invalid format or if data cannot be retrieved properly.
        /// </summary>
        /// <param name="orderId">The ticket order ID.</param>
        /// <returns><see cref="IEnumerable{Ticket}"/> tickets from this order.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderId"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="orderId"/> has invalid format.</exception>
        /// <exception cref="InternalDbServiceException">Retrieve data failed.</exception>
        Task<IEnumerable<Ticket>> GetOrderedTicketsAsync(string orderId);

        /// <summary>
        /// Asynchronously retrieves <see cref="Models.Customer"/> from ticket order specified by <paramref name="orderId"/>.
        /// Throws exception if <paramref name="orderId"/> has invalid format, cannot found customer or if data cannot be retrieved properly.
        /// </summary>
        /// <param name="orderId">The ticket order ID.</param>
        /// <returns><see cref="Models.Customer"/> from this order.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderId"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="orderId"/> has invalid format.</exception>
        /// <exception cref="InvalidOperationException"><see cref="Models.Customer"/> not found.</exception>
        /// <exception cref="InternalDbServiceException">Retrieve data failed.</exception>
        Task<Customer> GetCustomerAsync(string orderId);
    }
}