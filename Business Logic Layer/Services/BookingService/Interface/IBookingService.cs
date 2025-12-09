using Business_Logic_Layer.ViewModels.MembershipViewModels;
using Business_Logic_Layer.ViewModels.SessionViewModels;
using GymManagementBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.BookingService.Interface
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        IEnumerable<MemberForSessionViewModel> GetMembersForUpcomingBySessionId(int sessionId);
        IEnumerable<MemberForSessionViewModel> GetMembersForOngoingBySessionId(int sessionId);
        IEnumerable<MemberSelectListViewModel> GetMembersForDropDown(int sessionId);
        bool CancelBooking(int MemberId, int SessionId);
        bool CreateNewBooking(CreateBookingViewModel createdBooking);
        bool MemberAttended(int MemberId, int SessionId);
    }
}
