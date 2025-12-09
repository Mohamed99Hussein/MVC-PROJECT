using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        // Get all Plans
        IEnumerable<Plan> GetAllPlans();
        //  Get Plan by id
        Plan? GetPlan(int id);
        //  Add Plan
        int AddPlan(Plan plan);
        //  Update Plan
      
    }
}
