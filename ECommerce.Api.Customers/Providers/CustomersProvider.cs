using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
           
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "A", Address = "123" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "B", Address = "456" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "C", Address = "789" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "D", Address = "000" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers,
            string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Quering customers");
                var customers = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);


                if (customers != null)
                {
                    logger?.LogInformation("Customer found");
                    var result = mapper.Map<Db.Customer, Customer>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }

}