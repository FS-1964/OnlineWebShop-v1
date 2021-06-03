using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
       
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;
     
        public OrderRepository(DatabaseContext appDbContext, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager) : base(appDbContext)
        {
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        public async Task<int> CreateOrder(Order order, ClaimsPrincipal cp)
        {
            var createdorder= CreateOrderDetail(order, cp);
            await Add(createdorder);
            return await SaveChanges();
        }

        private Order CreateOrderDetail(Order order, ClaimsPrincipal cp)
        {
            order.OrderPlaced = DateTime.Now;
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderDetails = new List<OrderDetail>(_shoppingCart.ShoppingCartItems.Count());
            //adding the order with its details

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    CakeId = shoppingCartItem.Cake.CakeId,
                    Price = shoppingCartItem.Cake.Price
                };
                order.OrderDetails.Add(orderDetail);
                order.UserId = _userManager.GetUserId(cp);
            }
            return order;
        }

        public async Task<int> DeleteOrder(Guid Id)
        {
            var orderTodelete = await Get(Id);
            Remove(orderTodelete);
            return await SaveChanges();
        }

        public IQueryable<Order> GetOrders(string uId)
        {
            return GetAll(o => o.UserId == uId, "OrderDetails");
        }

       
    }
}
