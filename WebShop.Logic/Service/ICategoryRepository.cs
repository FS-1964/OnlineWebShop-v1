using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public interface ICategoryRepository 
    {
        IEnumerable<Category> AllCategories { get; }
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
