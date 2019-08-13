using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ITicketDbService
    {
        Task<Ticket> GetTicketAsync(string id);
        Task<IEnumerable<Ticket>> GetTicketsAsync(IEnumerable<string> ids);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<IEnumerable<Ticket>> GetTicketsByAsync(Expression<Func<Ticket, bool>> predicate);
        Task<Ticket> AddTicketAsync(Ticket ticket);
        Task<Ticket> UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(string id);
    }
}