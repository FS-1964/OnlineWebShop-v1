using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.DAL.Datacontext;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _appDbContext;

        public CategoryRepository(DatabaseContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> AllCategories => _appDbContext.Categories;

        public async Task<IEnumerable<Category>> GetAllCategories()
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
    }
}
