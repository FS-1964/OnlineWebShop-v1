using Microsoft.AspNetCore.Identity;
using System;


namespace WebShop.DAL.Datacontext
{
    public static class DataBaseInitializer    {
        public static void SeedData(UserManager<ApplicationUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        private  static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";

                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";

                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
        private static  void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("DemoUser").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Demouser";
                user.Email = "Demouser@localhost.com";
                user.FirstName = "Nancy";
                user.LastName = "blain";
                user.Birthdate = new DateTime(1960, 1, 1);

                IdentityResult result = userManager.CreateAsync
                (user, "Asus1964$").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "User").Wait();
                }
            }


            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@localhost.com";
                user.FirstName = "john";
                user.LastName = "blain";
                user.Birthdate = new DateTime(1965, 1, 1);

                IdentityResult result = userManager.CreateAsync
                (user, "Asus1964$").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();
                }
            }
        }

       
    }
}
