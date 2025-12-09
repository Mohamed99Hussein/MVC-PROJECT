using Business_Logic_Layer.Services.TranierService.Interface;
using Business_Logic_Layer.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

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











    }
}
