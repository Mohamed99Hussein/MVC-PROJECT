using AutoMapper;
using Business_Logic_Layer.ViewModels.SessionViewModels;
using Data_Access_Layer.Entities;
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
        
        
        }


        }
}
