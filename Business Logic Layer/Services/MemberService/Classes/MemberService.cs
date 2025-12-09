using AutoMapper;
using Business_Logic_Layer.Services.MemberService.Interfaces;
using Business_Logic_Layer.ViewModels.MemberViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using Data_Access_Layer.Unit_Of_Work.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.MemberService.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateMember(CreateMemberViewModel CreateMember)
        {
            try
            {

                if (CheckEmail(CreateMember.Email)|| CheckPhone(CreateMember.Phone))
                return false;
                

                var mappedMember = mapper.Map<CreateMemberViewModel, Member>(CreateMember);

                unitOfWork.GetRepository<Member>().Add(mappedMember);
                return unitOfWork.SaveChanges() > 0;

              
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Sorry, I can't Add Member, {ex}");
                return false; 
            }


        }     

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = unitOfWork.GetRepository<Member>().GetAll();

                if(!Members.Any() || Members is null) 
                 return Enumerable.Empty<MemberViewModel>(); // []

            #region First Way of Manual Mapping
            //var MemberViewModels = new List<MemberViewModel>();
            //foreach (var Member in Members)
            //{
            //    var MemberViewModel = new MemberViewModel
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Phone = Member.Phone,
            //        Email = Member.Email,
            //        Gender = Member.Gender.ToString(),
            //        Photo = Member.Photo,
            //    };
            //    MemberViewModels.Add(MemberViewModel);

            //} 
            #endregion

            #region Second Way of Manual Mapping
            //var MemberViewModels = Members.Select(M => new MemberViewModel
            //{
            //    Id = M.Id,
            //    Name = M.Name,
            //    Phone = M.Phone,
            //    Email = M.Email,
            //    Gender = M.Gender.ToString(),
            //    Photo = M.Photo,

            //});

            #endregion

                var MemberViewModels = mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(Members);

            return MemberViewModels;

        }

        public HealthRecordViewModel? GetHealthRecord(int MemberId)
        {
           
            var HealthRecord = unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (HealthRecord == null) return null;

            var mappedHealthRecord = mapper.Map<HealthRecord, HealthRecordViewModel>(HealthRecord);
            return mappedHealthRecord;

        
        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var Member = unitOfWork.GetRepository<Member>().GetById(MemberId);

            if(Member is not null)

           {
               
                var memberViewModel = mapper.Map<Member, MemberViewModel>(Member);

                var MemberShipActive = unitOfWork.GetRepository<MemberShip>().GetAll(x => x.MemberId == MemberId&& x.Status=="Active")
                    .FirstOrDefault();
                
                if (MemberShipActive != null)
                {
                    memberViewModel.MemberShipStartDate = MemberShipActive.CreatedAt.ToShortDateString();
                    memberViewModel.MemberShipEndDate = MemberShipActive.EndDate.ToShortDateString();
                    var Plan =unitOfWork.GetRepository<Plan>().GetById(MemberShipActive.PlanId);
                    memberViewModel.PlanName = Plan?.Name;
                }

                return memberViewModel;
            }

            return null;


            

        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var TargetMember = unitOfWork.GetRepository<Member>().GetById(MemberId);

            if (TargetMember is  null) return null;
       var mappedMemberToUpdate = mapper.Map<Member, MemberToUpdateViewModel>(TargetMember);
            return mappedMemberToUpdate;

        }

        public bool RemoveMember(int MemberId)
        {
            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);
             if (member is null) return false;

            // Don't remove that has Active MemberShip

            var SessionsId = unitOfWork.GetRepository<MemberSession>().GetAll(x=>x.MemberId == MemberId)
                .Select(x=>x.MemberId);
            
            var FutureSessions = unitOfWork.GetRepository<Session>()
                .GetAll(x=> SessionsId.Contains(x.Id) && x.StartTime < DateTime.Now).Any();


            if (FutureSessions) return false;

            var DeletedMemberShips = unitOfWork.GetRepository<MemberShip>().GetAll(x=>x.MemberId== MemberId);

            try
            {
                if (DeletedMemberShips.Any())
                {
                    foreach (var DeletedMemberShip in DeletedMemberShips)
                    {
                        unitOfWork.GetRepository<MemberShip>().Delete(DeletedMemberShip);

                    }
                }
                        unitOfWork.GetRepository<Member>().Delete(member);
                        return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry I Can't Delete Member , {ex}");
                return false;
            }
        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember)     
        {
           
          var EmailExistCheck = unitOfWork.GetRepository<Member>()
                .GetAll(x=>x.Email == UpdatedMember.Email && x.Id != MemberId);

            var PhoneExistCheck = unitOfWork.GetRepository<Member>()
               .GetAll(x => x.Phone == UpdatedMember.Phone && x.Id != MemberId);

            if( EmailExistCheck.Any() || PhoneExistCheck.Any() )
                return false;


            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);

            if (member is null) return false;

            mapper.Map(UpdatedMember, member);
            unitOfWork.GetRepository<Member>().Update(member) ;
            return unitOfWork.SaveChanges() > 0;

        }

        private bool CheckEmail(string email)
        {
            return unitOfWork.GetRepository<Member>().GetAll(X => X.Email== email).Any();
        }

        private bool CheckPhone(string phone)
        {
            return unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == phone).Any();
        }

    }
}
