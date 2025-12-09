using Business_Logic_Layer.Services.MemberService.Classes;
using Business_Logic_Layer.Services.MemberService.Interfaces;
using Business_Logic_Layer.Services.TranierService.Interface;
using Business_Logic_Layer.ViewModels.MemberViewModels;
using Business_Logic_Layer.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Presentation_Layer.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        public IActionResult Index()
        {
           var trainers = trainerService.GetTrainers();

            return View(trainers);
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id, Can not be less than 1";

                return View(nameof(Index));
            }

            var trainerDetails = trainerService.GetTrainerDetails(id);
            if(trainerDetails == null)
            {
                TempData["ErrorMessage"] = "Sorry, Trainer Not Found.";

                return View(nameof(Index));
            }

            return View(trainerDetails);
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createdTrainer)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Please Check Data Or Missing Fields.");
                return RedirectToAction(nameof(Create),createdTrainer);
            }

            var CheckCreate = trainerService.CreateTrainer(createdTrainer);

            if(CheckCreate)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully.";
            }

            else
            {
                TempData["ErrorMessage"] = "Trainer Failed to create, Please check Phone and Email.";
            }

            return RedirectToAction(nameof(Index));

        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id, Can not be less than 1";

                return RedirectToAction(nameof(Index));
            }

            var trainer = trainerService.GetTrainerToUpdate(id);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);


        }

       
            [HttpPost]
            public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel updatedTrainer)
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Your Update isn't valid. Please try again.";
                return View(Edit(id));
                }

                var CheckUpdateResult = trainerService.UpdateTrainer(id, updatedTrainer);
                if (CheckUpdateResult)
                {
                    TempData["SuccessMessage"] = "Trainer Updated Successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Trainer Failed to be Updated.";
                }

                return RedirectToAction(nameof(Index));

            }


        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id, Can not be less than 1";

                return RedirectToAction(nameof(Index));
            }

            var Trainer = trainerService.GetTrainerDetails(id);

            if (Trainer == null)
            {
                TempData["ErrorMessage"] = "Sorry, Trainer Not Found.";

                return View(nameof(Index));
            }

            ViewBag.TrainerId = Trainer.Id;
            return View();

        }

        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int id)
        {
           
              var Result = trainerService.DeleteTrainer(id);

                if (Result)
                    TempData["SuccessMessage"] = "Member Deleted Successfully.";
                else
                {
                    TempData["ErrorMessage"] = "Sorry, Member Failed to be Deleted.";
                }

                return RedirectToAction(nameof(Index));
            
        }





    }
}
