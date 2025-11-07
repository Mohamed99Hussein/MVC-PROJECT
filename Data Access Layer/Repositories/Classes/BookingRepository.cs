using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    public class BookingRepository : GenericRepository<MemberSession>, IBookingRepository
    {
        private readonly GymSystemDBContext _dbContext;

        public BookingRepository(GymSystemDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<MemberSession> GetBySessionId(int sessionId)
        {
            return _dbContext.MemberSessions.Include(X => X.Member)
                                      .Where(X => X.SessionId == sessionId).ToList();
        }
    }
}
