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

        public SessionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = unitOfWork.sessionRepository.GetAllSessionsWithTrainersAndCategories();

            if (Sessions == null || !Sessions.Any()) return [];

            return Sessions.Select(session => new SessionViewModel
            {
                Id = session.Id,
                Description = session.Description,
                StartDate = session.StartTime,
                EndDate = session.EndTime,
                Capacity = session.Capacity,
                CategoryName = session.Category.CategoryName,
                TrainerName = session.SessionTrainer.Name,
                AvailableSlots = session.Capacity - unitOfWork.sessionRepository.GetCountOfBookings(session.Id)

            });


        }
    }
}
