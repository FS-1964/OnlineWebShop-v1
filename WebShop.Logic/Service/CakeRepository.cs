using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public class CakeRepository : GenericRepository<Cake>, ICakeRepository
    {
        private readonly DatabaseContext _appDbContext;
        #region Class methods
        public CakeRepository(DatabaseContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Cake>> GetAllCakes()
        {
            try
            {
                return await All();

            }
            catch (Exception ex)
            {

                throw new Exception($" could not be proceed: {ex.Message}");
            }
        }
        public async Task<IEnumerable<Cake>> GetCakesByCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(category));
            }
            try
            {
                return await Find(c => c.Category == category);

            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(category)} could not be proceded: {ex.Message}");
            }

        }
        public async Task<IEnumerable<Cake>> GetCakesOfTheWeek()
        {

            try
            {
                return await Find(c => c.IsCakeOfTheWeek == true);

            }
            catch (Exception ex)
            {

                throw new Exception($" could not be proceed: {ex.Message}");
            }

        }
        public async Task<int> CreateCake(Cake cake)
        {
            if (cake == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(cake));
            }
            try
            {
                await Add(cake);
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(cake)} could not be created: {ex.Message}");
            }
        }
        public async Task<int> DeleteCake(Cake cake)
        {
            if (cake == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(cake));
            }
            try
            {
                Remove(cake);
                return await SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(cake)} could not be removed: {ex.Message}");
            }
        }
        public async Task<Cake> GetCakeById(Guid? cakeId)
        {
            if (cakeId == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(cakeId));
            }
            try
            {
               return await GetInclude(c => c.CakeId == cakeId, "CakeReviews").FirstOrDefaultAsync();
                //return await Get(cakeId);
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(cakeId)} could not be removed: {ex.Message}");
            }
        }
        public async Task<int> UpdateCake(Cake cake)
        {
            if (cake == null)
            {
                throw new ArgumentNullException("Input parameter is null", nameof(cake));
            }
            try
            {
                Update(cake);
                return await SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"{nameof(cake)} could not be removed: {ex.Message}");
            }

        }
        #endregion
    }
}
