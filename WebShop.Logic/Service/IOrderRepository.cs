using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public interface IOrderRepository 
    {
        Task<int> CreateOrder(Order order, ClaimsPrincipal cp);

        Task<int> DeleteOrder(Guid Id);
        IQueryable<Order> GetOrders(string uId);
    }
}
