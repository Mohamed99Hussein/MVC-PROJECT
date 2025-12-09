using Business_Logic_Layer.Services.MemberService.Interfaces;
using Business_Logic_Layer.ViewModels.MemberViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.MemberService.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IgenericRepository<Member> memberRepository;
        private readonly IgenericRepository<MemberShip> memberShipRepository;
        private readonly IPlanRepository planRepository;
        private readonly IgenericRepository<HealthRecord> healthRecordRepository;

        public MemberService(IgenericRepository<Member> memberRepository,
            IgenericRepository<MemberShip> memberShipRepository,
            IPlanRepository planRepository,
            IgenericRepository<HealthRecord> healthRecordRepository)
        {
            this.memberRepository = memberRepository;
            this.memberShipRepository = memberShipRepository;
            this.planRepository = planRepository;
            this.healthRecordRepository = healthRecordRepository;
        }

        public bool CreateMember(CreateMemberViewModel CreateMember)
        {
            try
            {
                
                if (CheckEmail(CreateMember.Email) && CheckPhone(CreateMember.Phone))
                                            return false;

                var Member = new Member()
                {
                    Email = CreateMember.Email,
                    Phone = CreateMember.Phone,
                    Gender = CreateMember.Gender,
                    DateOfBirth = CreateMember.DateOfBirth,
                    Address = new Addess
                    {
                        BuildingNumber = CreateMember.BuildingNumber,
                        Street = CreateMember.Street,
                        City = CreateMember.City,
                    },
                    HealthRecord = new HealthRecord
                    {
                        Height = CreateMember.HealthRecordViewModel.Height,
                        Weight = CreateMember.HealthRecordViewModel.weight,
                        BloodType = CreateMember.HealthRecordViewModel.BloodType,
                        Note = CreateMember.HealthRecordViewModel.Note,
                    }

                };

                return memberRepository.Add(Member) > 0;
            }

            catch (Exception)
            {
                Console.WriteLine("Sorry, I can't Add Member");
                return false; 
            }


        }     

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = memberRepository.GetAll();

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
            var MemberViewModels = Members.Select(M => new MemberViewModel
            {
                Id = M.Id,
                Name = M.Name,
                Phone = M.Phone,
                Email = M.Email,
                Gender = M.Gender.ToString(),
                Photo = M.Photo,

            });

            #endregion
            
            return MemberViewModels;

        }

        public HealthRecordViewModel? GetHealthRecord(int MemberId)
        {
           
            var HealthRecord = healthRecordRepository.GetById(MemberId);
            if (HealthRecord == null) return null;

            return new HealthRecordViewModel()   
            { 
                Height = HealthRecord.Height,
                weight = HealthRecord.Weight,
                BloodType = HealthRecord.BloodType,
                Note = HealthRecord.Note,
            };

             

        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            var Member = memberRepository.GetById(MemberId);

            if(Member is not null)

           {
                var memberViewModel = new MemberViewModel
                {
                    Name = Member.Name,
                    Phone = Member.Phone,
                    Email = Member.Email,
                    Gender = Member.Gender.ToString(),
                    Photo = Member.Photo,
                    DateOfBirth = Member.DateOfBirth.ToShortDateString(),
                };

                var MemberShipActive = memberShipRepository.GetAll(x => x.Id == MemberId&& x.Status=="Active")
                    .FirstOrDefault();
                
                if (MemberShipActive != null)
                {
                    memberViewModel.MemberShipStartDate = MemberShipActive.CreatedAt.ToShortDateString();
                    memberViewModel.MemberShipEndDate = MemberShipActive.EndDate.ToShortDateString();
                    var Plan = planRepository.GetPlan(MemberShipActive.PlanId);
                    memberViewModel.PlanName = Plan?.Name;
                }

                return memberViewModel;
            }

            return null;


            

        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var TargetMember = memberRepository.GetById(MemberId);

            if (TargetMember is  null) return null;

            return new MemberToUpdateViewModel()
            {
                Name = TargetMember.Name,
                Phone = TargetMember.Phone,
                Email = TargetMember.Email,
                BuildingNumber = TargetMember.Address.BuildingNumber,
                City = TargetMember.Address.City,
                Street = TargetMember.Address.Street,
                Photo = TargetMember.Photo
            };

        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember)
        {
           
            if (CheckEmail(UpdatedMember.Email) || CheckPhone(UpdatedMember.Phone))
                                    return false;

            var member = memberRepository.GetById(MemberId);

            if (member is null) return false;

            member.Email = UpdatedMember.Email;
            member.Phone = UpdatedMember.Phone;
            member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
            member.Address.City = UpdatedMember.City;
            member.Address.Street = UpdatedMember.Street;
            member.UpdatedAt = DateTime.Now;

            return memberRepository.Update(member) > 0;

        }

        private bool CheckEmail(string email)
        {
            return memberRepository.GetAll(X => X.Email== email).Any();
        }

        private bool CheckPhone(string phone)
        {
            return memberRepository.GetAll(X => X.Phone == phone).Any();
        }

    }
}
