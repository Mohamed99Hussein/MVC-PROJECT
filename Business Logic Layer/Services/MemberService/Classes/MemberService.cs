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

        public MemberService(IgenericRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
            
        }

        public bool CreateMember(CreateMemberViewModel CreateMember)
        {
            try
            {
                var EmailExists = memberRepository.GetAll(x => x.Email == CreateMember.Email);
                var PhonesExists = memberRepository.GetAll(x => x.Phone == CreateMember.Phone);

                if (EmailExists.Any() && PhonesExists.Any()) return false;

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
    }
}
