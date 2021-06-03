using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;
using WebShop.Logic.Service;
using WebShop.PL.Models;

//change comment
namespace WebShop.PL.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly ISoldItems _soldItems;
        private DatabaseContext _appDbContext;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart,ISoldItems soldItems, DatabaseContext appDbContext)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _soldItems = soldItems;
            _appDbContext = appDbContext;
        }

        // GET: /<controller>/

        public IActionResult Checkout()
        {
            return View();
        }
      
          
        [HttpPost]
      //  [Authorize(Policy = "MinimumOrderAge")]
        public async Task<IActionResult> Checkout(Order order)
        {
            try
            {
              
               var items = _shoppingCart.GetShoppingCartItems();
               _shoppingCart.ShoppingCartItems = items;
              
                if (_shoppingCart.ShoppingCartItems.Count() == 0)
                {
                    ModelState.AddModelError("", "Your cart is empty, add some cakes first");
                }
               
                if (ModelState.IsValid)
                {
                    order.OrderTotal = items.Sum(item => item.Amount * item.Cake.Price);
                    var result = await _orderRepository.CreateOrder(order, HttpContext.User);
                    if (result > 0)
                    {
                        var dbresult = await _soldItems.SetSoldItemToDb(order.OrderDetails);
                        TempData["DbSave"] = "Order successfull";
                    }
                    else
                    {
                        TempData["DbSave"] = "Order Failed";
                    }
                    await _appDbContext.SaveChangesAsync();
                    await _shoppingCart.ClearCart();
                    return RedirectToAction("CheckoutComplete");
                }
                return View(order);
            }
            catch (Exception ex)
            {

                throw new Exception($" could not be proceed: {ex.Message}");
            }
        }

        public IActionResult CheckoutComplete()
        {  
            return View();
        }

        public async Task<IActionResult> DeleteOrder(Guid Id)
        {
              await _orderRepository.DeleteOrder(Id);
            return RedirectToAction("CheckoutComplete");
        }
    }
}
