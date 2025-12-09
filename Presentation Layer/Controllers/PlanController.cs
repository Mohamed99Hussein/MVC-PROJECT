using Business_Logic_Layer.Services.PlanService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService)
        {
            this.planService = planService;
        }
        public IActionResult Index()
        {
            var AllPlans = planService.GetAllPlans();
            return View(AllPlans);
        }

        public ActionResult Details(int id)
        {
            if(id<=0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id, Can not be less than 1";

                return RedirectToAction(nameof(Index));
            }

            var planDetails = planService.GetPlan(id);

            if(planDetails == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(planDetails);

        }
    }
}
