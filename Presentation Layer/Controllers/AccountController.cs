using Business_Logic_Layer.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
    }
}
