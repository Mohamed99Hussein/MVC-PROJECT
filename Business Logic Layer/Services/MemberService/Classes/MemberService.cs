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
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;

        public MemberService(IUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork;
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

                unitOfWork.GetRepository<Member>().Add(Member);
                return unitOfWork.SaveChanges() > 0;

                //return memberRepository.Add(Member) > 0;
            }

            catch (Exception)
            {
                Console.WriteLine("Sorry, I can't Add Member");
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
           
            var HealthRecord = unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
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
            var Member = unitOfWork.GetRepository<Member>().GetById(MemberId);

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

                var MemberShipActive = unitOfWork.GetRepository<MemberShip>().GetAll(x => x.Id == MemberId&& x.Status=="Active")
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
            var TargetMember = unitOfWork.GetRepository<Member>().GetById(MemberId);

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

        public bool RemoveMember(int MemberId)
        {
            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);
             if (member is null) return false;

            // Don't remove that has Active MemberShip

            var HasActiveMemberSessions = unitOfWork.GetRepository<MemberSession>().GetAll(x=>x.MemberId == MemberId
            && x.Session.StartTime < DateTime.Now).Any();
            if (HasActiveMemberSessions) return false;

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
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember)
        {
           
            if (CheckEmail(UpdatedMember.Email) || CheckPhone(UpdatedMember.Phone))
                                    return false;

            var member = unitOfWork.GetRepository<Member>().GetById(MemberId);

            if (member is null) return false;

            member.Email = UpdatedMember.Email;
            member.Phone = UpdatedMember.Phone;
            member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
            member.Address.City = UpdatedMember.City;
            member.Address.Street = UpdatedMember.Street;
            member.UpdatedAt = DateTime.Now;

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
