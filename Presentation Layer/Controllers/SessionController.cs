using Business_Logic_Layer.Services.SessionService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var Sessions = sessionService.GetAllSessions();
            return View(Sessions);
        }
    }
}
