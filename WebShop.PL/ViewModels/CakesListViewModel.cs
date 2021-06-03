using System.Collections.Generic;
using WebShop.DAL.Entities;


namespace WebShop.PL.ViewModels
{
    public class CakesListViewModel
    {
        public IEnumerable<Cake> Cakes { get; set; }
        public string CurrentCategory { get; set; }
    }
}
