using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface ISessionRepository
    {
        // Get all Sessions
        IEnumerable<Session> GetAllSession();

        //  Get Session by id
        Session? GetSession(int id);

        //  Add Session
        int AddSession(Session session);

        //  Update Session
        int UpdateSession(Session session);

        //  Delete Session
        int DeleteSession(int id);
    }
}
