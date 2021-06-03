using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebShop.DAL.Auth
{
    public class MinimumOrderAgeRequirement : AuthorizationHandler<MinimumOrderAgeRequirement>, IAuthorizationRequirement
    {
        private readonly int _minimumOrderAge;
       
        public MinimumOrderAgeRequirement(int minimumOrderAge)
        {
            _minimumOrderAge = minimumOrderAge;
           
        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumOrderAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Age for ordering" ||  c.Type == "Manage Cakes"))
            {
                return Task.CompletedTask;
            }
           

            var userbirthdate = context.User.Claims.FirstOrDefault(c => c.Type == "Age for ordering")?.Value;
            if(userbirthdate!=null)
            {
                var age = Convert.ToDateTime(userbirthdate);
                var ageInYears = DateTime.Today.Year - age.Year;
                if (ageInYears >= _minimumOrderAge)
                {
                    context.Succeed(requirement);
                }
            }
            var managecake = context.User.Claims.FirstOrDefault(c => c.Type == "Manage Cakes")?.Value;
            if (managecake != null)
            {
                context.Succeed(requirement);
            }
                return Task.CompletedTask;
        }
    }
}
