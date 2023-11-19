using Microsoft.AspNetCore.Identity;
using Noteing.API.Models;

namespace Noteing.API.Helpers
{
    public static class InitalSetupHelper
    {
        internal static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult adminRoleResult;
            bool adminRoleExists = await RoleManager.RoleExistsAsync("Admin");

            if (!adminRoleExists)
            {
                adminRoleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}
