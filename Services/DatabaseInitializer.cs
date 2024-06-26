using Microsoft.AspNetCore.Identity;

namespace RegistrationWebApp.Services
{
    public class DatabaseInitializer
    {
        public static async Task SeedDataAsync(UserManager<IdentityUser>? userManager,
            RoleManager<IdentityRole>? roleManager)
        {
            if(userManager == null || roleManager == null)
            {
                Console.WriteLine("userManager or roleManager is null => exit");
                return;
            }

            var exist = await roleManager.RoleExistsAsync("admin");
            if(!exist)
            {
                Console.WriteLine("Admin role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            exist = await roleManager.RoleExistsAsync("client");
            if(!exist)
            {
                Console.WriteLine("Client role is defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("client"));
            }

            var adminUsers = await userManager.GetUsersInRoleAsync("admin");
            if(adminUsers.Any())
            {
                Console.WriteLine("Admin user already exist => exit");
                return;
            }

            var user = new IdentityUser()
            {
                UserName = "admin@beststore.com",
                Email = "admin@beststore.com",
            };

            string defaultPassword = "Admin123*";

            var result = await userManager.CreateAsync(user, defaultPassword);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
                Console.WriteLine("Admin user created successfully. Please update the initial password");
                Console.WriteLine("Email: " + user.Email + "- Initial password: " + defaultPassword);
            }
            else
            {
                Console.WriteLine("Unable to create Admin User: " + result.Errors.First().Description);
            }
        }
    }
}