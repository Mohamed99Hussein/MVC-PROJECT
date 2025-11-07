using AutoMapper;
using Business_Logic_Layer.ViewModels.MembershipViewModels;
using Business_Logic_Layer.ViewModels.MemberViewModels;
using Business_Logic_Layer.ViewModels.PlanViewModels;
using Business_Logic_Layer.ViewModels.SessionViewModels;
using Business_Logic_Layer.ViewModels.TrainerViewModels;
using Data_Access_Layer.Entities;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            MapSession();
            MapMember();
            MapPlan();
            MapTrainer();
            MapMembership();
        }

        private void MapSession()
        {
            #region Session - SessionViewModel
            CreateMap<Session, SessionViewModel>()
                .ForMember(SVM => SVM.CategoryName,
                Options => Options.MapFrom(S => S.Category.CategoryName))
                .ForMember(SVM => SVM.TrainerName,
                Options => Options.MapFrom(S => S.SessionTrainer.Name))
                .ForMember(SVM => SVM.AvailableSlots, Options => Options.Ignore())
                .ForMember(dest => dest.StartDate, Options =>
                Options.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndDate, Options =>
                Options.MapFrom(src => src.EndTime));



            #endregion

            #region CreatedSessionViewModel-Session

            CreateMap<CreateSessionViewModel, Session>()
                .ForMember(S => S.StartTime,
                Options => Options.MapFrom(CSVM => CSVM.StartDate))
                .ForMember(S => S.EndTime,
                Options => Options.MapFrom(CSVM => CSVM.EndDate));

            #endregion

            #region Session - UpdateSessionViewModel

            CreateMap<Session, UpdateSessionViewModel>()
                .ForMember(USVM => USVM.StartDate,
                Options => Options.MapFrom(S => S.StartTime))
                .ForMember(USVM => USVM.EndDate,
                Options => Options.MapFrom(S => S.EndTime));


            #endregion

            #region UpdateSessionViewModel - Session

            CreateMap<UpdateSessionViewModel, Session>()
                .ForMember(S => S.StartTime,
                Options => Options.MapFrom(USVM => USVM.StartDate))
                .ForMember(S => S.EndTime,
                Options => Options.MapFrom(USVM => USVM.EndDate));

            #endregion

            CreateMap<Trainer, TrainerToSelectViewModel>();

            CreateMap<Category, CategoryToSelectViewModel>()
                .ForMember(dest => dest.Name,
                Options =>
                Options.MapFrom(src => src.CategoryName));

        }

        private void MapMember()
        {
            #region CreateMemberViewModel-Member
            CreateMap<CreateMemberViewModel, Member>()
                   .ForMember(M => M.Address,
                   Options => Options.MapFrom(CMV => new Address
                   {
                       BuildingNumber = CMV.BuildingNumber,
                       Street = CMV.Street,
                       City = CMV.City,
                   }))
                   //.ForMember(M => M.HealthRecord,
                   //Options => Options.MapFrom(CMV => new HealthRecord
                   //{
                   //    Height = CMV.HealthRecordViewModel.Height,
                   //    Weight = CMV.HealthRecordViewModel.weight,
                   //    BloodType = CMV.HealthRecordViewModel.BloodType,
                   //    Note = CMV.HealthRecordViewModel.Note,
                   //}));
                   .ForMember(m => m.HealthRecord,
                   opt => opt.MapFrom(src => src.HealthRecordViewModel));

            //CreateMap<CreateMemberViewModel, HealthRecord>()
            //    .ForMember(HR => HR.Height,
            //    Options => Options.MapFrom(CMV => CMV.HealthRecordViewModel.Height))
            //    .ForMember(HR => HR.Weight,
            //    Options => Options.MapFrom(CMV => CMV.HealthRecordViewModel.weight))
            //    .ForMember(HR => HR.BloodType,
            //    Options => Options.MapFrom(CMV => CMV.HealthRecordViewModel.BloodType))
            //    .ForMember(HR => HR.Note,
            //    Options => Options.MapFrom(CMV => CMV.HealthRecordViewModel.Note));



            #endregion

            #region Member - MemberViewModels
            CreateMap<Member, MemberViewModel>()
                .ForMember(mvm => mvm.Gender,
                Options => Options.MapFrom(m => m.Gender.ToString()))
                .ForMember(mvm => mvm.DateOfBirth, Options =>
                Options.MapFrom(m => m.DateOfBirth.ToShortDateString()))
               .ForMember(mvm => mvm.Address,
                Options => Options.MapFrom(m => $"{m.Address.BuildingNumber} - {m.Address.Street} - {m.Address.City}"))






                ;
            #endregion

            #region HealthRecord - HealthRecordViewModels

            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

            #endregion

            #region Member - MemberToUpdateViewModel

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(MUVM => MUVM.BuildingNumber,
                Options => Options.MapFrom(M => M.Address.BuildingNumber))
                .ForMember(MUVM => MUVM.Street,
                Options => Options.MapFrom(M => M.Address.Street))
                .ForMember(MUVM => MUVM.City,
                Options => Options.MapFrom(M => M.Address.City));

            #endregion


            #region MemberToUpdateViewModel - Member
            CreateMap<MemberToUpdateViewModel, Member>()
            .ForMember(m => m.Name, Options => Options.Ignore())
            .ForMember(m => m.Photo, Options => Options.Ignore())
            .AfterMap((muvm, m) =>
            {
                m.Address.BuildingNumber = muvm.BuildingNumber;
                m.Address.Street = muvm.Street;
                m.Address.City = muvm.City;
                m.UpdatedAt = DateTime.Now;
            });

            #endregion


        }

        private void MapPlan()
        {
            #region Plan - PlanViewModel
            CreateMap<Plan, PlanViewModel>();
            #endregion

            #region Plan - PlanToUpdateViewModel
            CreateMap<Plan, PlanToUpdateViewModel>();
            #endregion

            #region PlanToUpdateViewModel - Plan
            CreateMap<PlanToUpdateViewModel, Plan>()
                 //.ForMember(p => p.Name, Options => Options.Ignore())
                 .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            #endregion

        }

        private void MapTrainer()
        {

            #region Trainer - TrainerViewModel
            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Address,
                opt => opt.MapFrom(
                    src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City} "));
            #endregion

            #region CreateTrainerViewModel - Trainer
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(t => t.Address,
                Options => Options.MapFrom(CTV => new Address
                {
                    BuildingNumber = CTV.BuildingNumber,
                    Street = CTV.Street,
                    City = CTV.City,
                }))
                .ForMember(t => t.CreatedAt,
                Options => Options.MapFrom(CTV => DateTime.Now));
            #endregion

            #region Trainer - TrainerToUpdateViewModel  

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(TUV => TUV.BuildingNumber,
                Options => Options.MapFrom(T => T.Address.BuildingNumber))
                .ForMember(TUV => TUV.Street,
                Options => Options.MapFrom(T => T.Address.Street))
                .ForMember(TUV => TUV.City,
                Options => Options.MapFrom(T => T.Address.City));

            #endregion

            #region TrainerToUpdateViewModel - Trainer
            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(t => t.Name, Options => Options.Ignore())
                .AfterMap((tuvm, t) =>
                {
                    t.Address.BuildingNumber = tuvm.BuildingNumber;
                    t.Address.Street = tuvm.Street;
                    t.Address.City = tuvm.City;
                    t.UpdatedAt = DateTime.Now;
                });

            #endregion



        }

        private void MapMembership()
        {
            CreateMap<MemberShip, MembershipViewModel>()
                .ForMember(MVM => MVM.MemberName,
                Options => Options.MapFrom(M => M.Member.Name))
                .ForMember(MVM => MVM.PlanName,
                Options => Options.MapFrom(M => M.Plan.Name));

            CreateMap<CreateMemberShipViewModel, MemberShip>();
            CreateMap<Plan, PlanSelectListViewModel>();
            CreateMap<Member, MemberSelectListViewModel>();








        }



























    }
}