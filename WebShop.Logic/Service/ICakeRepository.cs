using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.Logic.Service
{
    public interface ICakeRepository
    {
        Task<IEnumerable<Cake>> GetAllCakes();
        Task<IEnumerable<Cake>> GetCakesByCategory(Category category);
        Task<IEnumerable<Cake>> GetCakesOfTheWeek();
        Task<Cake> GetCakeById(Guid? cakeId);
        Task<int> CreateCake(Cake cake);
        Task<int> UpdateCake(Cake cake);
        Task<int> DeleteCake(Cake cake);
    }
}
