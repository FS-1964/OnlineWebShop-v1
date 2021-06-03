using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace WebShop.Logic.Service
{
   public static class DbSetExtensions
    {
        public static void DeleteAll<T>(this IdentityDbContext context)
        where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
        }
    }
}
