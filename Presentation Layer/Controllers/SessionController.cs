using Business_Logic_Layer.Services.MemberService.Classes;
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

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Session Id, Can not be less than 1";

                RedirectToAction(nameof(Index));
            }


            var SessionDetails = sessionService.GetSessionById(id);

            if (SessionDetails is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                RedirectToAction(nameof(Index));
            }


            return View(SessionDetails);
        }












































        }
}
