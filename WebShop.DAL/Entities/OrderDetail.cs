using System;

namespace WebShop.DAL.Entities
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CakeId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public Cake Cake { get; set; }
        public Order Order { get; set; }
    }
}
