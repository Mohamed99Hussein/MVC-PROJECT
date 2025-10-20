using AutoMapper;
using Business_Logic_Layer.Services.SessionService.Interface;
using Business_Logic_Layer.ViewModels.SessionViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Unit_Of_Work.Interface;
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
    }
}
