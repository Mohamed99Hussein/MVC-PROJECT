using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_Access_Layer.Data.DataSeeding
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                bool hasUsers = userManager.Users.Any();
                bool hasRoles = roleManager.Roles.Any();

                // نكمل حتى لو في داتا علشان نتأكد من التنفيذ الصحيح
                Console.WriteLine("🚀 Starting Identity Seeding...");

                // =============== Seed Roles ===============
                if (!hasRoles)
                {
                    Console.WriteLine("🧩 Seeding Roles...");

                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "SuperAdmin" },
                        new IdentityRole { Name = "Admin" }
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = roleManager.RoleExistsAsync(role.Name!).GetAwaiter().GetResult();
                        if (!roleExists)
                        {
                            var result = roleManager.CreateAsync(role).GetAwaiter().GetResult();
                            Console.WriteLine(result.Succeeded
                                ? $"✅ Role '{role.Name}' created."
                                : $"❌ Failed to create role '{role.Name}'.");
                        }
                    }
                }

                // =============== Seed Users ===============
                if (!hasUsers)
                {
                    Console.WriteLine("👤 Seeding Users...");

                    // SuperAdmin
                    var userAdmin = new ApplicationUser
                    {
                        FirstName = "Mohamed",
                        LastName = "Magdy",
                        UserName = "MohamedMagdy",
                        Email = "MohamedMagdy@gmail.com",
                        PhoneNumber = "01029629865",
                        EmailConfirmed = true
                    };

                    var createAdminResult = userManager.CreateAsync(userAdmin, "Password1@").GetAwaiter().GetResult();
                    if (!createAdminResult.Succeeded)
                    {
                        Console.WriteLine("❌ Failed to create SuperAdmin user:");
                        foreach (var error in createAdminResult.Errors)
                            Console.WriteLine($"   - {error.Code}: {error.Description}");
                    }
                    else
                    {
                        userManager.AddToRoleAsync(userAdmin, "SuperAdmin").GetAwaiter().GetResult();
                        Console.WriteLine("✅ SuperAdmin user created and assigned to role.");
                    }

                    // Admin
                    var admin = new ApplicationUser
                    {
                        FirstName = "Aliaa",
                        LastName = "Tarek",
                        UserName = "AliaaTarek",
                        Email = "AliaaTarek@gmail.com",
                        PhoneNumber = "01029629346",
                        EmailConfirmed = true
                    };

                    var createUserResult = userManager.CreateAsync(admin, "Password1@").GetAwaiter().GetResult();
                    if (!createUserResult.Succeeded)
                    {
                        Console.WriteLine("❌ Failed to create Admin user:");
                        foreach (var error in createUserResult.Errors)
                            Console.WriteLine($"   - {error.Code}: {error.Description}");
                    }
                    else
                    {
                        userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
                        Console.WriteLine("✅ Admin user created and assigned to role.");
                    }
                }

                Console.WriteLine("✅ Identity seeding completed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to Seed Data: {ex.Message}");
                return false;
            }
        }
    }
}
