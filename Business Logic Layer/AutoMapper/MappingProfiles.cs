using AutoMapper;
using Business_Logic_Layer.ViewModels.SessionViewModels;
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

            #region Session - SessionViewModel
            CreateMap<Session, SessionViewModel>()
                .ForMember(SVM => SVM.CategoryName,
                Options => Options.MapFrom(S => S.Category.CategoryName))
                .ForMember(SVM => SVM.TrainerName,
                Options => Options.MapFrom(S => S.SessionTrainer.Name))
                .ForMember(SVM => SVM.AvailableSlots, Options => Options.Ignore());

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
        }


    }
}
