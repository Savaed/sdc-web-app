using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ICustomerDbService
    {
        Task<Customer> GetAsync(string id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<IEnumerable<Customer>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(string id);
        Task<Customer> AddAsync(Customer customer);
    }
}
