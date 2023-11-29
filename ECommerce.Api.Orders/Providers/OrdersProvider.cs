using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Net;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrderDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = new DateTime(2020, 3, 5),
                    Total = 100,
                    Items = new List<OrderItem>
                        {
                            new OrderItem { ProductId = 1, Quantity = 2, UnitPrice = 50.00m },
                            new OrderItem { ProductId = 2, Quantity = 1, UnitPrice = 30.00m },
                        }
                });
                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = new DateTime(2020, 4, 10),
                    Total = 75.50m,
                    Items = new List<OrderItem>
                        {
                            new OrderItem { ProductId = 3, Quantity = 1, UnitPrice = 25.50m },
                        }
                });

                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 1,
                    OrderDate = new DateTime(2020, 5, 15),
                    Total = 120.75m,
                    Items = new List<OrderItem>
                        {
                            new OrderItem { ProductId = 2, Quantity = 3, UnitPrice = 35.25m },
                            new OrderItem { ProductId = 3, Quantity = 2, UnitPrice = 20.00m },
                        }
                });

                dbContext.Orders.Add(new Db.Order()
                {
                    Id = 4,
                    CustomerId = 3,
                    OrderDate = new DateTime(2020, 6, 20),
                    Total = 50.20m,
                    Items = new List<OrderItem>
                         {
                            new OrderItem { ProductId = 1, Quantity = 1, UnitPrice = 30.00m },
                            new OrderItem { ProductId = 3, Quantity = 1, UnitPrice = 20.20m },
                         }
                });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                logger?.LogInformation($"Querying orders for customer {customerId}");
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    logger?.LogInformation($"{orders.Count} order(s) found");
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
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

