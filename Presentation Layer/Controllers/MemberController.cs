using Business_Logic_Layer.Services.MemberService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
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
           
            if(MemberDetails is null)
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






    }
}
