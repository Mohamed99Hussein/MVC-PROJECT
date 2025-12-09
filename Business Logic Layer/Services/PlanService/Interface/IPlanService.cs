using Business_Logic_Layer.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.PlanService.Interface
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlan(int PlanId);

        PlanToUpdateViewModel? GetPlanToUpdate(int PlanId);

        bool UpdatePlan(int PlanId, PlanToUpdateViewModel UpdatedPlan);

        bool TogglePlanStatus(int PlanId);


    }
}
