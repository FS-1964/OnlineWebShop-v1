using System;

namespace WebShop.DAL.Entities
{
    public class CakeReview
    {
        public Guid CakeReviewId { get; set; }
        public Cake Cake { get; set; }
        public string Review { get; set; }
    }
}
