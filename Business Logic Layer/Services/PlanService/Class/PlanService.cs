using Business_Logic_Layer.Services.PlanService.Interface;
using Business_Logic_Layer.ViewModels.PlanViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Unit_Of_Work.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.PlanService.Class
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = unitOfWork.GetRepository<Plan>().GetAll();

            if (Plans is null || !Plans.Any()) return [];
           
            var planViewModels = new List<PlanViewModel>();

            foreach (var Plan in Plans)
            {

                var planViewModel = new PlanViewModel()
                {
                    Id = Plan.Id,
                    Name = Plan.Name,
                    Description = Plan.Description,
                    Price = Plan.Price,
                    DurationDays = Plan.DurationDays,
                    IsActive = Plan.IsActive,
                };
                planViewModels.Add(planViewModel);
                
            }
            return planViewModels;

        }

        public PlanViewModel? GetPlan(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null) return null;

            return new PlanViewModel()
            {
                Description = Plan.Description,
                Price = Plan.Price,
                DurationDays = Plan.DurationDays,
                Name = Plan.Name,
                Id = Plan.Id,
                IsActive = Plan.IsActive,

            };
            
        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
             if (Plan is null || HasActiveMemberShip(PlanId) || Plan.IsActive == false)
                            return null;

            return new PlanToUpdateViewModel()
            {
                Description = Plan.Description,
                Price = Plan.Price,
                DurationDays = Plan.DurationDays,
                Name = Plan.Name,
            };
        }

        public bool TogglePlanStatus(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMemberShip(PlanId))
                return false;

            Plan.IsActive = !Plan.IsActive;
            Plan.UpdatedAt = DateTime.Now;

          try
            {
                unitOfWork.GetRepository<Plan>().Update(Plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            
            }
        }

        public bool UpdatePlan(int PlanId, PlanToUpdateViewModel UpdatedPlan)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
              if (Plan is null || HasActiveMemberShip(PlanId))
                return false;

            (Plan.Name, Plan.Description, Plan.Price, Plan.DurationDays, Plan.UpdatedAt) =
                (
                  UpdatedPlan.Name,
                  UpdatedPlan.Description,
                  UpdatedPlan.Price,
                  UpdatedPlan.DurationDays,
                  DateTime.Now
                ); // Tuple Syntax
    

            unitOfWork.GetRepository<Plan>().Update(Plan);
                    return unitOfWork.SaveChanges() > 0;
        }


        private bool HasActiveMemberShip(int PlanId)
        {
            var MemberShipStatus = unitOfWork.GetRepository<MemberShip>()
                .GetAll(x=>x.PlanId ==PlanId && x.Status =="Active").Any();
            return MemberShipStatus;
            
        }
    }
}
