using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.DataSeeding
{
    public static class IdentityDbContextSeeding
    {

        public static bool SeedData(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager )
        {
            try
            {
                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();
                if (HasUsers && HasRoles) return false;

                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new IdentityRole(){ Name ="SuperAdmin"},
                        new IdentityRole(){ Name ="Admin"},

                    };

                    foreach (var role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result)
                            roleManager.CreateAsync(role).Wait();
                    }

                }


                if (!HasUsers)
                {
                    var UserAdmin = new ApplicationUser()
                    {
                        
                            FirstName ="Mohamed",
                            LastName= "Magdy",
                            UserName = "MohamedMagdy",
                            Email ="MohamedMagdy@gmail.com",
                            PhoneNumber="01029629865"
                    };
                    userManager.CreateAsync(UserAdmin, "Passw0rd").Wait();
                    userManager.AddToRoleAsync(UserAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {

                        FirstName = "Mohamed",
                        LastName = "Osama",
                        UserName = "MohamedOsama",
                        Email = "MohamedOsama@gmail.com",
                        PhoneNumber = "01029629346"
                    };
                    userManager.CreateAsync(Admin, "Passw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();

                }



                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to Seed Data : {ex}");
                return false;
            }
        }


    }
}
