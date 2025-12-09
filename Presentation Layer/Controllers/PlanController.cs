using Business_Logic_Layer.Services.PlanService.Interface;
using Business_Logic_Layer.ViewModels.PlanViewModels;
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

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id, Can not be less than 1";

                return RedirectToAction(nameof(Index));
            }

            var planToEdit = planService.GetPlanToUpdate(id);
            if(planToEdit == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(planToEdit);



        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id,PlanToUpdateViewModel planToUpdate )
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Plan can't be updated.");
            }

            var CheckPlan = planService.UpdatePlan(id, planToUpdate);

            if(CheckPlan)
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed to be Updated.";
            }

            return RedirectToAction(nameof(Index));



        }

        [HttpPost]
        public ActionResult Activate([FromRoute] int id)
        {
            var Result = planService.TogglePlanStatus(id);

            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed Successfully.";

            }
            else
            {
                TempData["ErrorMessage"] = "Sorry, Failed to Change Plan Status.";
            }

            return RedirectToAction(nameof(Index));


        }
    
    
    
    }
}
