using AutoMapper;
using Business_Logic_Layer.Services.SessionService.Interface;
using Business_Logic_Layer.ViewModels.SessionViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Unit_Of_Work.Interface;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.SessionService.Class
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createdSessionViewModel)
        {
            //check if Trainer exists
           if(!IsTrainerExists(createdSessionViewModel.TrainerId)) return false;
            // check if Category exists
            var category = unitOfWork.GetRepository<Category>().GetById(createdSessionViewModel.CategoryId);
          if(!IsCategoryExists(createdSessionViewModel.CategoryId)) return false;
            // validate dates
          
          if(!AreDatesValid(createdSessionViewModel.StartDate,createdSessionViewModel.EndDate)) return false;

            var mappedSession = mapper.Map<CreateSessionViewModel,Session>(createdSessionViewModel);
            
            
            unitOfWork.GetRepository<Session>().Add(mappedSession);
                    return unitOfWork.SaveChanges() > 0 ;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = unitOfWork.sessionRepository.GetAllSessionsWithTrainersAndCategories();

            if (Sessions == null || !Sessions.Any()) return [];

            var MappedSessions = mapper.Map<IEnumerable<Session>,IEnumerable<SessionViewModel>>(Sessions);
            
            foreach (var session in MappedSessions)
            {
                session.AvailableSlots = session.Capacity - unitOfWork.sessionRepository.GetCountOfBookings(session.Id);
            }

            return MappedSessions;


        }

        public SessionViewModel? GetSessionById(int SessionId)
        {
            var session = unitOfWork.sessionRepository.GetSessionWithCategoryAndTrainerById(SessionId);
            if (session == null) return null;

           var MappedSession = mapper.Map<Session,SessionViewModel>(session);
            
           MappedSession.AvailableSlots = session.Capacity - unitOfWork.sessionRepository.GetCountOfBookings(session.Id);
            
            return MappedSession;
        }

        public bool UpdateSession(int SessionId, UpdateSessionViewModel updatedSessionViewModel)
        {
           var session = unitOfWork.GetRepository<Session>().GetById(SessionId);
            // check if session is available for updating
            // has no bookings and dates are in future
            // and check if He changed id through request
            if (!IsSessionAvailableForUpdating(session!)) return false;
            //check if Trainer exists
            if (!IsTrainerExists(updatedSessionViewModel.TrainerId)) return false;
            // validate dates
            if (!AreDatesValid(updatedSessionViewModel.StartDate, updatedSessionViewModel.EndDate)) return false;
          
            var mappedSession = mapper.Map<UpdateSessionViewModel,Session>(updatedSessionViewModel,session!);
          
            session!.UpdatedAt = DateTime.Now;

            unitOfWork.GetRepository<Session>().Update(session!);

                    return unitOfWork.SaveChanges() > 0;
        }

        public UpdateSessionViewModel? GetSessionForUpdate(int SessionId)
        {
            var session = unitOfWork.GetRepository<Session>().GetById(SessionId);

            if (!IsSessionAvailableForUpdating(session!)) return null;

            var mappedSession = mapper.Map<Session,UpdateSessionViewModel>(session!);
            return mappedSession;
        }

        public bool DeleteSession(int SessionId)
        {
            var session = unitOfWork.GetRepository<Session>().GetById(SessionId);
            // check if session is available for deleting
            if (!IsSessionAvailableForDeleting(session!)) return false;

            unitOfWork.GetRepository<Session>().Delete(session!);
                    return unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<TrainerToSelectViewModel> GetAllTrainersForDropDownList()
        {
            var trainers = unitOfWork.GetRepository<Trainer>().GetAll();

          return  mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerToSelectViewModel>>(trainers);
            
        }

        public IEnumerable<CategoryToSelectViewModel> GetAllCategoriesForDropDownList()
        {
           var categories = unitOfWork.GetRepository<Category>().GetAll();

            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryToSelectViewModel>>(categories);
        }




        #region Private Helper Methods

        private bool IsSessionAvailableForUpdating(Session session)
        {
            if(session is null) return false;

            if(unitOfWork.sessionRepository.GetCountOfBookings(session.Id) > 0) return false;
            
            if(session.StartTime <= DateTime.Now) return false;

            if(session.EndTime <= DateTime.Now) return false;

                    return true;    


        }

        private bool IsTrainerExists(int trainerId)
        {
           return unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;

        }

        private bool IsCategoryExists(int categoryId)
        {
            return unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }

        private bool AreDatesValid(DateTime startDate, DateTime endDate) => startDate<endDate && DateTime.Now < startDate ;
        
        private bool IsSessionAvailableForDeleting(Session session)
        {
            if (session is null) return false;

            if (unitOfWork.sessionRepository.GetCountOfBookings(session.Id) > 0) return false;

            if (session.StartTime <= DateTime.Now) return false;

            if (session.EndTime <= DateTime.Now) return false;

            if(session.StartTime>=DateTime.Now && session.EndTime>=DateTime.Now) return false;

                return true;


        }

        


        #endregion

    }
}
