using Microsoft.AspNetCore.Identity;
using Noteing.API.Models;

namespace Noteing.API.Helpers
{
    public static class InitalSetupHelper
    {
        internal static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await TryAddRole(roleManager, "Admin");
            await TryAddRole(roleManager, "Normal");
            await TryAddRole(roleManager, "Premium");
        }

        private static async Task TryAddRole(RoleManager<IdentityRole> roleManager, string name)
        {
            if (!await roleManager.RoleExistsAsync(name))
            {
                await roleManager.CreateAsync(new IdentityRole(name));
            }
        }
    }
}
