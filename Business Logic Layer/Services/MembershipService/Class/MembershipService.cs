using AutoMapper;
using Business_Logic_Layer.Services.MembershipService.Interface;
using Business_Logic_Layer.ViewModels.MembershipViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using Data_Access_Layer.Unit_Of_Work.Class;
using Data_Access_Layer.Unit_Of_Work.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.MembershipService.Class
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMembershipRepository membershipRepository;

        public MembershipService(IUnitOfWork unitOfWork,IMapper mapper,IMembershipRepository membershipRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.membershipRepository = membershipRepository;
        }

        public bool CreateMembership(CreateMemberShipViewModel CreatedMemberShip)
        {
            if (!IsMemberExists(CreatedMemberShip.MemberId) || !IsPlanExists(CreatedMemberShip.PlanId)
                    || HasActiveMemberShip(CreatedMemberShip.MemberId)) return false;
            
            var MemberShipToCreate = mapper.Map<MemberShip>(CreatedMemberShip);
            var Plan = unitOfWork.GetRepository<Plan>().GetById(CreatedMemberShip.PlanId);
            MemberShipToCreate.EndDate = DateTime.Now.AddDays(Plan!.DurationDays);

            unitOfWork.GetRepository<MemberShip>().Add(MemberShipToCreate);
            return unitOfWork.SaveChanges() > 0;
        }

        public bool DeleteMemberShip(int MemberId)
        {
            var Repo = unitOfWork.GetRepository<MemberShip>();
            var ActiveMemberships = Repo.GetAll(X => X.MemberId == MemberId && X.Status == "Active").FirstOrDefault();
            if (ActiveMemberships is null) return false;
            Repo.Delete(ActiveMemberships);
            return unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MembershipViewModel> GetAllMemberships()
        {
            var memberships = membershipRepository.GetAllMembershipsWithMemberAndPlan(X => X.Status == "Active");
            return mapper.Map<IEnumerable<MembershipViewModel>>(memberships);
             

        }

        public IEnumerable<MemberSelectListViewModel> GetMembersForDropDown()
        {
            var members = unitOfWork.GetRepository<Member>().GetAll();
            return mapper.Map<IEnumerable<MemberSelectListViewModel>>(members);
        }

        public IEnumerable<PlanSelectListViewModel> GetPlansForDropDown()
        {
            var plans = unitOfWork.GetRepository<Plan>()
                .GetAll(x=>x.IsActive==true);
            return mapper.Map<IEnumerable<PlanSelectListViewModel>>(plans);
        }

        
        
        #region Helper Methods 

        private bool IsMemberExists(int MemberId)
        {
            return unitOfWork.GetRepository<Member>().GetAll(X => X.Id == MemberId).Any();
        }
        private bool IsPlanExists(int PlanId)
        {
            return unitOfWork.GetRepository<Plan>().GetAll(X => X.Id == PlanId).Any();
        }
        private bool HasActiveMemberShip(int memberId)
        {
            return unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == memberId && X.Status == "Active").Any();
        }


        #endregion


    }
}
