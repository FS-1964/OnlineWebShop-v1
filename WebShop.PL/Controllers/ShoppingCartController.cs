using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.DAL.Entities;
using WebShop.Logic.Service;
using WebShop.PL.Helpers;
using WebShop.PL.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShop.PL.Controllers
{

    //[Authorize(Roles = "User")]
    [Authorize]
    [Route("ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private readonly ICakeRepository _cakeRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ICakeRepository cakeRepository, ShoppingCart shoppingCart)
        {
            _cakeRepository = cakeRepository;
            _shoppingCart = shoppingCart;
         
        }
        [Route("index")]
        public  async Task<ViewResult> Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "ShoppingCart");
            var items =  _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
              var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = await _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }
        public  async Task<RedirectToActionResult> CancelOrder()
        {
             await _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
        [Route("AddToShoppingCart/{cakeId}")]
        public async Task<RedirectToActionResult> AddToShoppingCart(Guid cakeId)
        {
            Cake selectedCake = null;
            selectedCake = await _cakeRepository.GetCakeById(cakeId);
            var result = await _shoppingCart.AddToCart(selectedCake, 1);
            if (result == 0) TempData["stock"] = string.Format($"Product: {selectedCake.Name}is not available");
            var cart = _shoppingCart.GetShoppingCartItems();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "ShoppingCart", cart);
            return RedirectToAction("Index");
        }
       
        private int isExist(Guid id)
        {
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "ShoppingCart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Cake.CakeId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        [Route("RemoveFromShoppingCart/{cakeId}")]
        public async Task<RedirectToActionResult> RemoveFromShoppingCart(Guid cakeId)
        {
           
            int index = isExist(cakeId);
            var selectedCake = await _cakeRepository.GetCakeById(cakeId);
           
            if (selectedCake != null && index !=-1)
            {
                await  _shoppingCart.RemoveFromCart(selectedCake);
            }
            return RedirectToAction("Index");
        }
    }
}
