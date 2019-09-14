using Autofac.Features.Indexed;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    /// <summary>
    /// Handles ticket order operations.
    /// </summary>
    public class TicketOrderHandler : ITicketOrderHandler
    {
        private readonly IIndex<string, IServiceBase> _dbServiceFactory;
        private readonly ISightseeingGroupDbService _groupDbService;
        private readonly IValidator<SightseeingGroup> _groupValidator;
        private readonly ILogger<TicketOrderHandler> _logger;
        private readonly string _orderStamp;
        private IEnumerable<ShallowTicket> _shallowTickets;
        private List<Ticket> _tickets;
        private Customer _customer;


        /// <summary>
        /// Gets the order ID associated with the customer and the tickets he ordered.
        /// </summary>
        public string OrderId => Encode();

        /// <summary>
        /// Gets customer associated with particular ticket order.
        /// </summary>
        public Customer Customer => _customer is null ? null : _customer;

        /// <summary>
        /// Gets tickets associated with particular ticket order.
        /// </summary>
        public IEnumerable<Ticket> Tickets => _tickets.AsEnumerable();


        public TicketOrderHandler(IIndex<string, IServiceBase> dbServiceFactory, IValidator<SightseeingGroup> groupValidator, ILogger<TicketOrderHandler> logger)
        {
            _orderStamp = Guid.NewGuid().ToString();
            _tickets = new List<Ticket>();
            _dbServiceFactory = dbServiceFactory;
            _groupDbService = _dbServiceFactory[nameof(ISightseeingGroupDbService)] as ISightseeingGroupDbService;
            _groupValidator = groupValidator;
            _logger = logger;
        }


        /// <summary>
        /// Asynchronously creates <see cref="Models.Customer"/> customer and <see cref="Ticket"/> tickets for this particular order.
        /// Throws exception if argument is invalid or if cannot create ticket order properly.
        /// </summary>
        /// <param name="order">The order request data for which a ticket order will be created.</param>
        /// <exception cref="ArgumentNullException"><paramref name="order"/> is null.</exception>
        /// <exception cref="ArgumentException"><see cref="OrderRequestDto.Customer"/> or <see cref="OrderRequestDto.Tickets"/> is null.</exception>
        /// <exception cref="InternalDbServiceException">Cannot create ticket order properly.</exception>
        public async Task CreateOrderAsync(OrderRequestDto order)
        {
            _logger.LogInformation($"Starting method '{nameof(CreateOrderAsync)}'.");

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order), $"Argument {nameof(order)} cannot be null.");
            }

            if (order.Tickets is null || order.Customer is null)
            {
                throw new ArgumentException($"Argument '{nameof(order)}' has property '{nameof(OrderRequestDto.Customer)}' or '{nameof(OrderRequestDto.Tickets)}' set to null.");
            }

            try
            {
                var customer = CreateCustomer(order);
                var savedCustomer = await GetSavedCustomerAsync(customer);

                if (savedCustomer is null)
                {
                    _logger.LogDebug("Adding new customer to the database.");
                    _customer = await AddCustomerToDbAsync(customer);
                }

                _customer = savedCustomer;
                _shallowTickets = order.Tickets;
                _logger.LogInformation($"Finished method '{nameof(CreateOrderAsync)}'.");
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"Customer creation failed. {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously creates new ones or updates existent <see cref="SightseeingGroup"/> groups by adding ordered <see cref="Ticket"/> tickets to them.
        /// Throws exception if the group cannot be properly handled due to its invalid state or if any problems while processing order data in the database occurred.
        /// </summary>
        /// <exception cref="InvalidOperationException">The group cannot be properly handled.</exception>
        /// <exception cref="InternalDbServiceException">Encountered problems while processing order data in the database.</exception>
        public async Task HandleOrderAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(HandleOrderAsync)}'.");

            try
            {
                var groups = await HandleGroupsAsync();
                _tickets = GetAllOrderedTickets(groups).ToList();
                _logger.LogInformation($"Finished method '{nameof(HandleOrderAsync)}'.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Invalid status of a group that is currently handling. {ex.Message}");
                throw;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"Encountered problems while processing order data in the database. {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves <see cref="Ticket"/> from ticket order specified by <paramref name="orderId"/>.
        /// Throws exception if <paramref name="orderId"/> has invalid format or if data cannot be retrieved properly.
        /// </summary>
        /// <param name="orderId">The ticket order ID.</param>
        /// <returns><see cref="IEnumerable{Ticket}"/> tickets from this order.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="orderId"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="orderId"/> has invalid format.</exception>
        /// <exception cref="InternalDbServiceException">Retrieve data failed.</exception>
        public async Task<IEnumerable<Ticket>> GetOrderedTicketsAsync(string orderId)
        {
            _logger.LogInformation($"Starting method '{nameof(GetOrderedTicketsAsync)}'.");

            if (orderId is null)
            {
                throw new ArgumentNullException(nameof(orderId), $"Argument {nameof(orderId)} cannot be null.");
            }

            try
            {
                string[] decodedOrderId = Decode(orderId);
                string orderStamp = decodedOrderId[1];

                var tickets = await (_dbServiceFactory[nameof(ITicketDbService)] as ITicketDbService).GetByAsync(x => x.OrderStamp.Equals(orderStamp));

                _logger.LogInformation($"Finished method '{nameof(GetOrderedTicketsAsync)}'.");
                return tickets;
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"Retrieve data from order with id: '{orderId}' failed.");
                throw;
            }
        }

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
        public async Task<Customer> GetCustomerAsync(string orderId)
        {
            _logger.LogInformation($"Starting method '{nameof(GetCustomerAsync)}'.");

            if (orderId is null)
            {
                throw new ArgumentNullException($"Argument '{nameof(orderId)}' cannot be null.");
            }

            string customerId = "";

            try
            {
                string[] decodedOrderId = Decode(orderId);
                customerId = decodedOrderId[0];

                var customer = await (_dbServiceFactory[nameof(ICustomerDbService)] as ICustomerDbService).GetAsync(customerId);
                _logger.LogInformation($"Finished method '{nameof(GetCustomerAsync)}'.");
                return customer;
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Specified '{nameof(Customer)}' with id: '{customerId}' not found.");
                return null;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"Retrieve data from order with id: '{orderId}' failed.");
                throw;
            }
        }


        #region Privates

        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="InternalDbServiceException"></exception>
        private async Task<IEnumerable<SightseeingGroup>> HandleGroupsAsync()
        {
            List<SightseeingGroup> sightseeingGroups = new List<SightseeingGroup>();

            try
            {
                var recentInfo = GetRecentInfo();

                // Get tickets grouped by SightseeingDate desc.
                var groupedTickets = _shallowTickets.GroupBy(x => x.SightseeingDate).OrderByDescending(y => y.Key).ToArray();

                for (int i = 0; i < groupedTickets.Count(); i++)
                {
                    var sightseeingDateTime = groupedTickets[i].Key;

                    // Check if exist group with this SightseeingDate.
                    var existentGroups = await _groupDbService.GetByAsync(x => x.SightseeingDate == sightseeingDateTime);

                    if (existentGroups.Count() == 0)
                    {
                        // Create new group
                        var newGroup = await HandleNonexistentGroupAsync(recentInfo, sightseeingDateTime, groupedTickets[i]);
                        sightseeingGroups.Add(newGroup);
                    }
                    else
                    {
                        // Update existent group.
                        var existentGroup = existentGroups.First();
                        var updatedGroup = await HandleExistentGroupAsync(existentGroup, groupedTickets[i]);
                        sightseeingGroups.Add(updatedGroup);
                    }
                }

                return sightseeingGroups.AsEnumerable();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                var exception = new InvalidOperationException($"The group cannot be properly handled. { ex.Message }", ex);
                throw exception;
            }
            catch (InvalidOperationException ex)
            {
                // There was a problem while processing the order. 
                // In order to preserve the invariability of data in the database, 
                // all changes made from the beginning of order processing should be undone.
                // Delete all added tickets and groups, undo edits of existing groups.
                // Log exception and throw it up.
                _logger.LogError(ex, $"{ex.Message}. All changes made during the order will be undone.");
                await UndoOrderChangesAsync(sightseeingGroups);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <exception cref="ArgumentNullException"><paramref name="order"/> is null.</exception>
        /// <exception cref="ArgumentException">$"Argument <see cref="OrderRequestDto"/> has property <see cref="OrderRequestDto.Customer"/> set to unexpected null.</exception>
        private Customer CreateCustomer(OrderRequestDto order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order), $"Argument {nameof(order)} cannot be null.");
            }

            if (order.Customer is null)
            {
                throw new ArgumentException($"Argument {nameof(order)} has property {nameof(OrderRequestDto.Customer)} set to unexpected null.", nameof(order));
            }

            return new Customer
            {
                DateOfBirth = order.Customer.DateOfBirth,
                EmailAddress = order.Customer.EmailAddress,
                HasFamilyCard = order.Customer.HasFamilyCard,
                IsChild = order.Customer.IsChild,
                IsDisabled = order.Customer.IsDisabled
            };
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="InternalDbServiceException"></exception>
        private async Task<IEnumerable<Ticket>> CreateTicketsAsync(IEnumerable<ShallowTicket> tickets)
        {
            if (tickets is null)
            {
                throw new ArgumentNullException(nameof(tickets), $"Argument {nameof(tickets)} cannot be null.");
            }

            List<Ticket> tmpTickets = new List<Ticket>();

            try
            {
                foreach (var ticket in tickets)
                {
                    tmpTickets.Add(new Ticket
                    {
                        TicketUniqueId = Guid.NewGuid().ToString(),
                        OrderStamp = _orderStamp,
                        Discount = await (_dbServiceFactory[nameof(IDiscountDbService)] as IDiscountDbService).GetAsync(ticket.DiscountId),
                        Tariff = await (_dbServiceFactory[nameof(ITicketTariffDbService)] as ITicketTariffDbService).GetAsync(ticket.TicketTariffId)
                    });
                }

                return tmpTickets.AsEnumerable();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Tickets creation failed. Specified '{nameof(Discount)}' or '{nameof(TicketTariff)}' not found. {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Tickets creation failed. {ex.Message}");
                throw;
            }
        }

        /// <exception cref="ArgumentNullException"><paramref name="customer"/> or <paramref name="tickets"/> is null.</exception>
        private void AddCustomerToTickets(Customer customer, IEnumerable<Ticket> tickets)
        {
            if (customer is null || tickets is null)
            {
                throw new ArgumentNullException($"Arguments {nameof(customer)} and {nameof(tickets)} cannot be null.");
            }

            foreach (var ticket in tickets)
            {
                ticket.Customer = customer;
            }
        }

        /// <exception cref="InvalidOperationException">Cannot add <see cref="Customer"/> properly.</exception>
        /// <exception cref="InternalDbServiceException">Cannot add <see cref="Customer"/> properly.</exception>
        private async Task<Customer> AddCustomerToDbAsync(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), $"Argument {nameof(customer)} cannot be null.");
            }

            try
            {
                return await (_dbServiceFactory[nameof(ICustomerDbService)] as ICustomerDbService).AddAsync(customer);
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogWarning(ex, $"Add customer to the database failed. {ex.Message}");
                throw;
            }
        }

        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InternalDbServiceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task<Customer> GetSavedCustomerAsync(Customer customer)
        {
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer), $"Argument {nameof(customer)} cannot be null.");
            }

            try
            {
                var result = await (_dbServiceFactory[nameof(ICustomerDbService)] as ICustomerDbService).GetByAsync(c => c.EmailAddress.Equals(customer.EmailAddress));
                return result.Count() == 0 ? null : result.Single();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Retrieve customer failed. {ex.Message}");
                throw;
            }
        }

        /// <exception cref="InternalDbServiceException"></exception>
        private VisitInfo GetRecentInfo()
        {
            try
            {
                return (_dbServiceFactory[nameof(IVisitInfoDbService)] as IVisitInfoDbService)
                       .GetAllAsync()
                       .Result
                       .OrderByDescending(x => x.UpdatedAt == DateTime.MinValue ? x.CreatedAt : x.UpdatedAt)
                       .First();
            }
            catch (InternalDbServiceException)
            {
                throw;
            }
        }

        private string Encode()
        {
            if (string.IsNullOrEmpty(_customer.Id))
            {
                return null;
            }

            // Encoded id - Base64String eg. MzU4ZDAxZDgtYzY1NC00MWQxLTliYzctYzk0OGE5M2RmNDFkO2NhMThlZGI1LTU3NzMtNGI5OC1hY2YyLTViMDk2ZDFmZGFhOA==
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_customer.Id};{_orderStamp}"));
        }

        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        private string[] Decode(string encodedId)
        {
            if (encodedId is null)
            {
                throw new ArgumentNullException(nameof(encodedId), $"Argument '{nameof(encodedId)}' cannot be null.");
            }

            try
            {
                // Decoded id - customerId;orderStamp eg. 358d01d8-c654-41d1-9bc7-c948a93df41d;ca18edb5-5773-4b98-acf2-5b096d1fdaa8
                string decodedId = Encoding.UTF8.GetString(Convert.FromBase64String(encodedId));
                return decodedId.Split(';', StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="InternalDbServiceException"></exception>
        private async Task<SightseeingGroup> HandleExistentGroupAsync(SightseeingGroup existedGroup, IEnumerable<ShallowTicket> shallowTickets)
        {
            int availablePlaces = existedGroup.MaxGroupSize - existedGroup.CurrentGroupSize;

            if (shallowTickets.Count() > availablePlaces)
            {
                throw new InvalidOperationException($"Attempt to add tickets to the group failed. There are '{availablePlaces}' place(s) " +
                    $"available on date '{existedGroup.SightseeingDate.ToString()}', but trying add '{shallowTickets.Count()}'.");
            }

            try
            {
                var tickets = await CreateTicketsAsync(shallowTickets) as ICollection<Ticket>;
                AddCustomerToTickets(_customer, tickets);

                // Update an existent group with new ordered tickets.


                var t = existedGroup.Tickets.ToList();
                t.AddRange(tickets);
                existedGroup.Tickets = t;


                return await _groupDbService.RestrictedUpdateAsync(existedGroup);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"The group cannot be properly handled. Group sightseeing date: '{existedGroup.SightseeingDate.ToString()}'. { ex.Message}");
                throw;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"Cannot saved data properly. Entity: '{nameof(SightseeingGroup)}'. Id: '{existedGroup.Id}'. {ex.Message}");
                throw;
            }
        }

        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="InternalDbServiceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private async Task<SightseeingGroup> HandleNonexistentGroupAsync(VisitInfo recentInfo, DateTime sightseeingDate, IEnumerable<ShallowTicket> shallowTickets)
        {
            if (shallowTickets.Count() > recentInfo.MaxAllowedGroupSize)
            {
                throw new InvalidOperationException($"Attempt to add tickets to the group failed. There are '{recentInfo.MaxAllowedGroupSize}' available places on date '{sightseeingDate.ToString()}'.");
            }

            SightseeingGroup newGroup = null;

            try
            {
                newGroup = new SightseeingGroup
                {
                    MaxGroupSize = recentInfo.MaxAllowedGroupSize,
                    SightseeingDate = sightseeingDate
                };

                // Check if new group based on order data is valid (SightseeingDate etc.).
                var validationResult = _groupValidator.Validate(newGroup);

                if (!validationResult.IsValid)
                {
                    throw new InvalidOperationException($"The group cannot be properly handled due to validation problems. {validationResult.Errors.First().ErrorMessage}");
                }

                var tickets = await CreateTicketsAsync(shallowTickets) as ICollection<Ticket>;
                AddCustomerToTickets(_customer, tickets);
                newGroup.Tickets = tickets;
                return await _groupDbService.RestrictedAddAsync(newGroup);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (InternalDbServiceException ex)
            {
                _logger.LogError(ex, $"The group cannot be properly handled. Group sightseeing date: '{newGroup.SightseeingDate.ToString()}'. { ex.Message}");
                throw;
            }
        }

        private IEnumerable<Ticket> GetAllOrderedTickets(IEnumerable<SightseeingGroup> groups)
        {
            return groups.SelectMany(group => group.Tickets.Where(ticket => ticket.OrderStamp.Equals(_orderStamp)));
        }

        private async Task UndoOrderChangesAsync(IEnumerable<SightseeingGroup> groups)
        {
            var ticketDbService = _dbServiceFactory[nameof(ITicketDbService)] as ITicketDbService;
            var tickets = GetAllOrderedTickets(groups);

            // Delete added tickets.
            var ticketsCopy = new List<Ticket>(tickets);
            foreach (var ticket in ticketsCopy)
            {
                await ticketDbService.DeleteAsync(ticket.Id);
            }

            // Undo updated or added groups.
            foreach (var group in groups)
            {
                await _groupDbService.RestrictedUpdateAsync(group);
            }

            // Clear ordered tickets.
            _tickets.Clear();
        }

        #endregion

    }
}
