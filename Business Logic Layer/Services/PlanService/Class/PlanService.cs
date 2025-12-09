using AutoMapper;
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
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = unitOfWork.GetRepository<Plan>().GetAll();

            if (Plans is null || !Plans.Any()) return [];
           
            var planViewModels = mapper.Map<IEnumerable<PlanViewModel>>(Plans);

            return planViewModels;

        }

        public PlanViewModel? GetPlan(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null) return null;

            var planViewModel = mapper.Map<PlanViewModel>(Plan);

            return planViewModel;



        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
             if (Plan is null || HasActiveMemberShip(PlanId) || Plan.IsActive == false)
                            return null;

             var planToUpdateViewModel = mapper.Map<PlanToUpdateViewModel>(Plan);
                return planToUpdateViewModel;

        }

        public bool UpdatePlan(int PlanId, PlanToUpdateViewModel UpdatedPlan)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
              if (Plan is null || HasActiveMemberShip(PlanId))
                return false;

            //(Plan.Name, Plan.Description, Plan.Price, Plan.DurationDays, Plan.UpdatedAt) =
            //    (
            //      UpdatedPlan.Name,
            //      UpdatedPlan.Description,
            //      UpdatedPlan.Price,
            //      UpdatedPlan.DurationDays,
            //      DateTime.Now
            //    ); // Tuple Syntax

              var planToUpdateViewModel = mapper.Map<PlanToUpdateViewModel, Plan>(UpdatedPlan, Plan);
                //planToUpdateViewModel.UpdatedAt = DateTime.Now;

            unitOfWork.GetRepository<Plan>().Update(Plan);
                    return unitOfWork.SaveChanges() > 0;
        }

        public bool TogglePlanStatus(int PlanId)
        {
            var Plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMemberShip(PlanId))
                return false;

            Plan.IsActive = Plan.IsActive == false ? true : false;
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
        
        private bool HasActiveMemberShip(int PlanId)
        {
            var MemberShipStatus = unitOfWork.GetRepository<MemberShip>()
                .GetAll(x=>x.PlanId ==PlanId && x.Status =="Active").Any();
            return MemberShipStatus;
            
        }
    }
}
