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
