using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserMgmt.Data;

namespace UserMgmt.Models
{
    /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-5.0&tabs=visual-studio#seed-the-database
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin", "admin@usermgmt.com");
                await EnsureRole(serviceProvider, adminID, "Admin");

            }
        }

        private static async Task<int> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string userName, string email)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = userName,
                    EmailConfirmed = true,
                    Email = email,
                    ///TODO Should remove
                    FirstName= "Admin",
                    LastName="Admin",
                    Gender=Gender.Na,
                    SystemRole=SystemRole.Admin
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      int uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<AppRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new AppRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<AppUser>>();
            //TODO why isn't FindbyIdAsync() working
            var user = await userManager.FindByEmailAsync("admin@usermgmt.com");


            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}