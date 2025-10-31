using Business_Logic_Layer.Services.MemberService.Interfaces;
using Business_Logic_Layer.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        public ActionResult Index()
        {
            var Data = memberService.GetAllMembers();

            return View(Data);
        }

        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id, Can not be less than 1";

                return View(nameof(Index));
            }


            var MemberDetails = memberService.GetMemberDetails(id);

            if (MemberDetails is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return View(nameof(Index));
            }


            return View(MemberDetails);

        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id, Can not be less than 1";
                return View(nameof(Index));
            }

            var HealthRecordDetails = memberService.GetHealthRecord(id);
            if (HealthRecordDetails is null)
            {
                TempData["ErrorMessage"] = "Health Record of Member Not Found";
                return View(nameof(Index));
            }

            return View(HealthRecordDetails);
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel CreatedMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Invalid Data Provided, Please check data and missing fields and Try Again");
                return View(nameof(Create), CreatedMember);
            }

            bool Result = memberService.CreateMember(CreatedMember);
            if (Result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully.";
            }

            else
            {
                TempData["ErrorMessage"] = "Member Failed to create, Please check Phone and Email.";
            }
            return RedirectToAction(nameof(Index));

        }

        public ActionResult EditMember(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id, Can not be less than 1";

                return RedirectToAction(nameof(Index));
            }

            var MemberToUpdate = memberService.GetMemberToUpdate(id);

            if (MemberToUpdate is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(MemberToUpdate);
        }

        [HttpPost]
        public ActionResult EditMember([FromRoute] int id, MemberToUpdateViewModel EditMember)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Your Update isn't valid. Please try again.";
                return View(EditMember);
            }

            var CheckUpdateResult = memberService.UpdateMemberDetails(id, EditMember);
            if (CheckUpdateResult)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed to be Updated.";
            }

            return RedirectToAction(nameof(Index));



        }


        public ActionResult DeleteMember(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id, Can not be less than 1";

                return RedirectToAction(nameof(Index));
            }

            var member = memberService.GetMemberDetails(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = member.Name;

            return View();

        }
        [HttpPost]
        public ActionResult DeleteConfirmed([FromForm] int id)
        {
            var Result = memberService.RemoveMember(id);

            if(Result)
                TempData["SuccessMessage"] = "Member Deleted Successfully.";
            else
            {
                TempData["ErrorMessage"] = "Sorry, Member Failed to be Deleted.";
            }

            return RedirectToAction(nameof(Index));
        }
           












    }
}
