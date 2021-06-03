using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public class CakeReviewRepository : GenericRepository<CakeReview>, ICakeReviewRepository
    {
        private readonly DatabaseContext _appDbContext;

        public CakeReviewRepository(DatabaseContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> AddCakeReview(CakeReview CakeReview)
        {
            

            if (CakeReview == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(CakeReview));
            }
            try
            {
                await Add(CakeReview);
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(CakeReview)} could not be created: {ex.Message}");
            }
        }

       

        public async Task<IEnumerable<CakeReview>> GetReviewsForCake(Guid cakeId)
        {
           
            if (cakeId == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(cakeId));
            }
            try
            {
                return await Find(p => p.Cake.CakeId == cakeId);

            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(cakeId)} could not be removed: {ex.Message}");
            }
        }

       
    }
}
