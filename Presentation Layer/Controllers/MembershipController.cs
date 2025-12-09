using Business_Logic_Layer.Services.MembershipService.Interface;
using Business_Logic_Layer.ViewModels.MembershipViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation_Layer.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipService membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }
        public ActionResult Index()
        {
            var memberships = membershipService.GetAllMemberships();
            return View(memberships);
        }

        public ActionResult Create()
        {
            LoadDropdowns();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMemberShipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = membershipService.CreateMembership(model);

                if (result)
                {
                    TempData["Success"] = "Membership created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Failed to create membership. member have an active membership.";
                }
            }
            LoadDropdowns();
            return View(model);
        }
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var result = membershipService.DeleteMemberShip(id);

            if (result)
            {
                TempData["Success"] = "Membership cancelled successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to cancel membership.";
            }

            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods
        private void LoadDropdowns()
        {
            var members = membershipService.GetMembersForDropDown();
            var plans = membershipService.GetPlansForDropDown();

            ViewBag.members = new SelectList(members, "Id", "Name");
            ViewBag.plans = new SelectList(plans, "Id", "Name");
        }
        #endregion

    }
}
