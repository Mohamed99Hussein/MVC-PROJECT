using Business_Logic_Layer.ViewModels.AccountViewModels;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
            var user = userManager.FindByEmailAsync(loginViewModel.Email).Result;
                if (user == null) return null;

                var CheckPassword = userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;   
        
                if(!CheckPassword) return null;

                return user;
        
        
        
        
        }
    }
}
