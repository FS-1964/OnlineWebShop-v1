using System;
using System.Collections.Generic;
using WebShop.DAL.Auth;

namespace WebShop.DAL.Entities
{
    public class Cake
    {

        public Guid CakeId { get; set; }
        //[Remote("CheckIfCakeNameAlreadyExists", "CakeManagement", ErrorMessage = "That name already exists")]
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AllergyInformation { get; set; }
        public decimal Price { get; set; }
        [ValidUrl(ErrorMessage = "That's not a valid URL")]
        public string ImageUrl { get; set; }
        [ValidUrl(ErrorMessage = "That's not a valid URL")]
        public string ImageThumbnailUrl { get; set; }
        public bool IsCakeOfTheWeek { get; set; }
        public int InStock { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual List<CakeReview> CakeReviews { get; set; }
        public int SoldItem { get; set; }
    }

}
