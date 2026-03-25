using GymManagementSystem.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Api.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            string[] roles = { "Admin", "Trainer", "Member" };

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            const string adminEmail = "admin@gmail.com";
            const string adminPassword = "Admin123!";
            const string adminFullName = "System Admin";

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin is null)
            {
                var adminUser = new AppUser
                {
                    FullName = adminFullName,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createAdminResult = await userManager.CreateAsync(adminUser, adminPassword);

                if (!createAdminResult.Succeeded)
                {
                    var errors = string.Join(" | ", createAdminResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create default admin user: {errors}");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");

                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join(" | ", addToRoleResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to assign Admin role: {errors}");
                }
            }
            else
            {
                var adminRoles = await userManager.GetRolesAsync(existingAdmin);

                if (!adminRoles.Contains("Admin"))
                {
                    var addToRoleResult = await userManager.AddToRoleAsync(existingAdmin, "Admin");

                    if (!addToRoleResult.Succeeded)
                    {
                        var errors = string.Join(" | ", addToRoleResult.Errors.Select(e => e.Description));
                        throw new InvalidOperationException($"Failed to assign Admin role to existing admin user: {errors}");
                    }
                }
            }
        }
    }
}