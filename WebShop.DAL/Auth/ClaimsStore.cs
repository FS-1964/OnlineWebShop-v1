using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebShop.DAL.Auth
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
    {
        new Claim("Age for ordering", "Age for ordering"),
        new Claim("Manage Cakes","Manage Cakes")
        
    };
    }
}
