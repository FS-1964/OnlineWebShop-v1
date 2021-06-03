using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public interface ICakeReviewRepository
    {
        Task<int> AddCakeReview(CakeReview cakeReview);
        Task<IEnumerable<CakeReview>> GetReviewsForCake(Guid cakeId);
    }
}
