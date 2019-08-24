using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SDCWebApp.Data;
using SDCWebApp.Models;

namespace SDCWebApp.Services
{
    public class CustomerDbService : ICustomerDbService
    {
        private readonly ILogger<CustomerDbService> _logger;
        private readonly ApplicationDbContext _context;


        public CustomerDbService(ApplicationDbContext context, ILogger<CustomerDbService> logger)
        {
            _logger = logger;
            _context = context;
        }


        public Task<Customer> AddAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
