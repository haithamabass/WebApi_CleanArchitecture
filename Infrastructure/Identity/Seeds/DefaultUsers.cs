using Infrastructure.Identity.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            #region defaultUser1
            var defaultUser1 = new AppUser
            {
                UserName = "haitham.abass49@gmail.com",
                Email = "haitham.abass49@gmail.com",
                FirstName = "Haitham",
                LastName = "Abass",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser1.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser1.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser1, "123456");
                    await userManager.AddToRoleAsync(defaultUser1, Roles.SuperAdmin.ToString());
                }
            }
            #endregion


            #region defaultUser2

            var defaultUser2 = new AppUser
            {
                UserName = "Maged.sobhy50@gmail.com",
                Email = "Maged.sobhy50@gmail.com",
                FirstName = "Maged",
                LastName = "Sobhy",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser2.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser2.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser2, "123456");
                    await userManager.AddToRoleAsync(defaultUser2, Roles.User.ToString());
                }
            }
            #endregion

        }
    }
}