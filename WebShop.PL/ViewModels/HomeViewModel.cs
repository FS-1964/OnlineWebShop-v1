﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DAL.Entities;

namespace WebShop.PL.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Cake> CakesOfTheWeek { get; set; }
    }
}
