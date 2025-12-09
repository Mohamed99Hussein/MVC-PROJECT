using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly GymSystemDBContext context;
        public PlanRepository(GymSystemDBContext context)
        {
            this.context = context;
        }
        public int AddPlan(Plan plan)
        {
            context.Plans.Add(plan);
            return context.SaveChanges();

        }

        public int DeletePlan(int id)
        {
            var plan = context.Plans.Find(id);
            if (plan != null)
            {
                context.Plans.Remove(plan);
                return context.SaveChanges();
            }
            return 0;

        }

        public IEnumerable<Plan> GetAllPlans()
        {
            return context.Plans.ToList();

        }

        public Plan? GetPlan(int id)
        {
            return context.Plans.Find(id);

        }

        public int UpdatePlan(Plan plan)
        {
            var existingPlan = context.Plans.Find(plan.Id);
            if (existingPlan != null)
            {
                context.Plans.Update(plan);
                return context.SaveChanges();
            }
            return 0;

        }
    }
}
