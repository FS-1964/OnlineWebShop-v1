using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using WebShop.DAL.Entities;

namespace WebShop.PL.ViewModels
{
    public class CakeEditViewModel
    {
        public Cake Cake { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public Guid CategoryId { get; set; }
    }
}
