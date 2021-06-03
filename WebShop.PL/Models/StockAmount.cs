using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.PL.Models
{
    public class StockAmount
    {
        public  Guid ItemId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
