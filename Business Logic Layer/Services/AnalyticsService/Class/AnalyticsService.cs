using Business_Logic_Layer.Services.AnalyticsService.Interface;
using Business_Logic_Layer.ViewModels.AnalyticsViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Unit_Of_Work.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AnalyticsService.Class
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions = unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel
            {
                
                TotalMembers = unitOfWork.GetRepository<Member>().GetAll().Count(),
                ActiveMembers = unitOfWork.GetRepository<MemberShip>().GetAll().Count(m => m.Status=="Active"),
                Trainers = unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = sessions.Count(s => s.StartTime > DateTime.Now),
                OngoingSessions = sessions.Count(s => s.StartTime <= DateTime.Now && s.EndTime >= DateTime.Now),
                CompletedSessions = sessions.Count(s => s.EndTime < DateTime.Now)
            };
        }
    }
}
