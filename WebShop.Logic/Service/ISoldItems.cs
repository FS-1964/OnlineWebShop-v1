using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public interface ISoldItems
    {
        Task<int> SetSoldItemToDb(List<OrderDetail> orderdetail);
        Task<int> GetSoldItemFromDb(Guid cakeid);
        Task<int> GetCakeInStock(Guid cakeid);
    }
}