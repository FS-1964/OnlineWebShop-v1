using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public interface IShoppingCart
    {
        string ShoppingCartId { get; set; }
        IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<int> AddToCart(Cake cake, int amount);
        Task<int> ClearCart();
        List<ShoppingCartItem> GetShoppingCartItems();
        Task<decimal> GetShoppingCartTotal();
        Task<int> RemoveFromCart(Cake cake);
      
    }
}