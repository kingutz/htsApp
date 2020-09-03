using htsApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace htsApp
{
    public  class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var _roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!context.Users.Any(usr => usr.UserName == "admin@hts.com"))
                {
                    var user = new ApplicationUser()
                    {
                        UserName = "admin@hts.com",
                        Email = "admin@hts.com",
                        EmailConfirmed = true,
                    };

                    var userResult = _userManager.CreateAsync(user, "hts2020").Result;
                }


                if (!_roleManager.RoleExistsAsync("admin").Result)
                {
                    var role = _roleManager.CreateAsync(new IdentityRole { Name = "admin" }).Result;
                }

                var adminUser = _userManager.FindByNameAsync("admin@hts.com").Result;
                var adminRole = _userManager.AddToRolesAsync(adminUser, new string[] { "admin" }).Result;


               








            }

        }

    }
}
