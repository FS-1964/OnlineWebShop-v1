using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;


namespace WebShop.Logic.Service
{
    public class ShoppingCart : GenericRepository<ShoppingCartItem>, IShoppingCart 
    {
        private  DatabaseContext _appDbContext;
        public string ShoppingCartId { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
      
        private ShoppingCart(DatabaseContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<DatabaseContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
        
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public async Task<int> AddToCart(Cake cake, int amount)
        {
            if (cake.InStock == 0 || amount == 0)
            {
                return 0;
            }

            var shoppingCartItem =
                    await _appDbContext.ShoppingCartItems.SingleOrDefaultAsync(
                         s => s.Cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);
            var liste = await Find(s => s.Cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);
            var itemlist = liste.FirstOrDefault();

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Cake = cake,
                    Amount = Math.Min(cake.InStock, amount)
                };

                await _appDbContext.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            else
            {

                if (cake.InStock - shoppingCartItem.Amount - amount < 0)
                {
                    shoppingCartItem.Amount += (cake.InStock - shoppingCartItem.Amount);

                }
                else
                {
                    shoppingCartItem.Amount += amount;
                }
            }

            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCart(Cake cake)
        {
            try
            {
                var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId);
                if (shoppingCartItem == null || cake == null) return 0;

                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }

                return await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(cake)} could not be remove: {ex.Message}");
            }
        }
       
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
          
            return GetAll(c => c.ShoppingCartId == ShoppingCartId, "Cake").ToList();
        }

        public async Task<int> ClearCart()
        { 
            var cartItems =await Find(cart => cart.ShoppingCartId == ShoppingCartId);
            RemoveRange(cartItems);
            return await SaveChanges();
        }

        public async Task<decimal> GetShoppingCartTotal()
        {
            var total = await _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
               .Select(c => c.Cake.Price * c.Amount).SumAsync();
            return total;
        }

        
    }
}
