using Business_Logic_Layer.Services.AnalyticsService.Class;
using Business_Logic_Layer.Services.AnalyticsService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService analyticsService;
        

        public HomeController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        //[NonAction] // This method is not an action method and cannot be invoked via HTTP requests
        public IActionResult Index()
        {
        //return View() // Returns the default view for the Index action
        //return View(model) //  Returns the default view for the Index action with a model
        //return View("hamada") // Returns the specific view "hamada" 
        //return View("hamada",model) // Returns the specific view "hamada" with a model 
            
            var Data = analyticsService.GetAnalyticsData();


            return View(Data);
        
        }
    }
}
