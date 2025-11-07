using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    public interface IBookingRepository : IgenericRepository<MemberSession>
    {
        IEnumerable<MemberSession> GetBySessionId(int sessionId);
    }
}
