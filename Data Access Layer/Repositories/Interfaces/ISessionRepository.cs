using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    public interface ISessionRepository
    {
      public IEnumerable<Session> GetAllSessionsWithTrainersAndCategories();

      public int GetCountOfBookings(int sessionId);

      public Session? GetSessionWithCategoryAndTrainerById(int sessionId);

       


    }
}
