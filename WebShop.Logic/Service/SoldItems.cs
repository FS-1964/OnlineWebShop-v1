using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public class SoldItems : GenericRepository<Cake>, ISoldItems
    {
        private readonly DatabaseContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SoldItems(DatabaseContext appDbContext, UserManager<ApplicationUser> userManager) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async Task<int> GetCakeInStock(Guid cakeid)
        {
            if (cakeid == null) return -1;
            
                var cake= await Get(cakeid);
                return cake.InStock;
           
        }

        public async Task<int> GetSoldItemFromDb(Guid cakeid)
        {
            if (cakeid == null) return -1;
            var cake = await Get(cakeid);
            return cake.SoldItem;
        }

        public async Task<int> SetSoldItemToDb(List<OrderDetail> orderdetail)
        {
            if (orderdetail == null) return 0;
            foreach (var item in orderdetail)
            {
                if(item.Amount <= item.Cake.InStock)
                {
                    item.Cake.SoldItem += item.Amount;
                    item.Cake.InStock -= item.Amount;
                    Update(item.Cake);
                    return await SaveChanges();
                }
            }
            return 0;
        }
    }
}
