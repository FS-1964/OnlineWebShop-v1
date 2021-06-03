using System;
using System.Collections.Generic;

namespace WebShop.DAL.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Cake> Cakes { get; set; }
    }
}
