using Business_Logic_Layer.Services.MemberService.Classes;
using Business_Logic_Layer.Services.SessionService.Interface;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public ActionResult Create()
        {
            LoadDrop();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createdSession)
        {
            if(!ModelState.IsValid)
            {
                LoadDrop();
                return View(createdSession);    

            }

            var Result = sessionService.CreateSession(createdSession);

            if(Result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully.";
               return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed to create.";
                LoadDrop();
                return View(createdSession);
            }

        }






































        private void LoadDrop()
        {
            var categories = sessionService.GetAllCategoriesForDropDownList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            var trainers = sessionService.GetAllTrainersForDropDownList();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");

        }




    }
}
