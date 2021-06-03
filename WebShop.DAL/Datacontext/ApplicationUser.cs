using Microsoft.AspNetCore.Identity;
using System;
using WebShop.DAL.Auth;

namespace WebShop.DAL.Datacontext
{
    public class ApplicationUser :IdentityUser
    {
        [DateValidator]
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }
}