using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebShop.Logic.Service;
using WebShop.PL.ViewModels;

namespace WebShop.PL.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        private Task<decimal> GetTotalAsync()
        {
            return  _shoppingCart.GetShoppingCartTotal();
        }
        public IViewComponentResult Invoke()
        {

            var items =  _shoppingCart.ShoppingCartItems;
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal =  GetTotalAsync().Result

            };
            return View(shoppingCartViewModel);
        }
    }
}
