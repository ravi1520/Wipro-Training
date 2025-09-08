using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Ensure Roles exist
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Create default admin
        string adminEmail = "admin@taskapp.com";
        string adminPassword = "Admin@123";  // change in production

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // 3. Add claim to "User" role members (CanEditTask)
        var allUsers = userManager.Users.ToList();
        foreach (var user in allUsers)
        {
            if (await userManager.IsInRoleAsync(user, "User"))
            {
                var claims = await userManager.GetClaimsAsync(user);
                if (!claims.Any(c => c.Type == "CanEditTask"))
                {
                    await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("CanEditTask", "true"));
                }
            }
        }
    }
}
