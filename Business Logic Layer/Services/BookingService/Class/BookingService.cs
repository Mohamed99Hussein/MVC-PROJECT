using AutoMapper;
using Business_Logic_Layer.Services.BookingService.Interface;
using Business_Logic_Layer.ViewModels.MembershipViewModels;
using Business_Logic_Layer.ViewModels.SessionViewModels;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using Data_Access_Layer.Unit_Of_Work.Interface;
using GymManagementBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.BookingService.Class
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly ISessionRepository _sessionRepository;


        public BookingService(IUnitOfWork unitOfWork, IMapper mapper, IBookingRepository bookingRepository, ISessionRepository sessionRepository, IMembershipRepository membershipRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _sessionRepository = sessionRepository;

        }
        public bool CancelBooking(int MemberId, int SessionId)
        {
            try
            {
                var session = _unitOfWork.GetRepository<Session>().GetById(SessionId);
                if (session is null || session.StartTime <= DateTime.Now) return false;

                var Booking = _bookingRepository.GetAll(X => X.SessionId == SessionId && X.MemberId == MemberId)
                                                           .FirstOrDefault();
                if (Booking is null) return false;
                _bookingRepository.Delete(Booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool CreateNewBooking(CreateBookingViewModel createdBooking)
        {
            try
            {
                var session = _unitOfWork.GetRepository<Session>().GetById(createdBooking.SessionId);
                if (session is null || session.StartTime <= DateTime.Now) return false;

                var HasActiveMembership = _unitOfWork.GetRepository<MemberShip>().GetAll(X => X.MemberId == createdBooking.MemberId && X.Status == "Active").Any();
                if (!HasActiveMembership) return false;

                var HasAvailableSolts = session.Capacity - _sessionRepository.GetCountOfBookings(createdBooking.SessionId);
                if (HasAvailableSolts == 0) return false;
                _bookingRepository.Add(new MemberSession()
                {
                    MemberId = createdBooking.MemberId,
                    SessionId = createdBooking.SessionId,
                    IsAttended = false
                });

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var bookings = _sessionRepository
                .GetAllSessionsWithTrainersAndCategories().Where(X => X.EndTime >= DateTime.Now)
                .OrderByDescending(X => X.StartTime);

            if (!bookings.Any()) return null!;
            var MappedSession = _mapper.Map<IEnumerable<SessionViewModel>>(bookings);
            foreach (var item in MappedSession)
            {
                item.AvailableSlots = item.Capacity - _sessionRepository.GetCountOfBookings(item.Id);
            }
            return MappedSession;
        }
        public IEnumerable<MemberForSessionViewModel> GetMembersForUpcomingBySessionId(int sessionId)
        {
            var MemberForSession = _bookingRepository.GetBySessionId(sessionId);
            return MemberForSession.Select(X => new MemberForSessionViewModel
            {
                MemberId = X.MemberId,
                SessionId = sessionId,
                MemberName = X.Member.Name,
                BookingDate = X.CreatedAt.ToString()
            });
        }
        public IEnumerable<MemberForSessionViewModel> GetMembersForOngoingBySessionId(int sessionId)
        {
            var MemberForSession = _bookingRepository.GetBySessionId(sessionId);
            return MemberForSession.Select(X => new MemberForSessionViewModel
            {
                MemberId = X.MemberId,
                SessionId = sessionId,
                MemberName = X.Member.Name,
                BookingDate = X.CreatedAt.ToString(),
                IsAttended = X.IsAttended
            });
        }
        public IEnumerable<MemberSelectListViewModel> GetMembersForDropDown(int sessionId)
        {
            var bookedMemberIds = _unitOfWork.GetRepository<MemberSession>()
                                   .GetAll(x => x.SessionId == sessionId)
                                   .Select(x => x.MemberId)
                                   .ToList();

            var availableMembers = _unitOfWork.GetRepository<Member>()
                                              .GetAll(x => !bookedMemberIds.Contains(x.Id));

            return _mapper.Map<IEnumerable<MemberSelectListViewModel>>(availableMembers);
        }
        public bool MemberAttended(int MemberId, int SessionId)
        {
            try
            {
                var memberSession = _unitOfWork.GetRepository<MemberSession>()
                                           .GetAll(X => X.MemberId == MemberId && X.SessionId == SessionId)
                                           .FirstOrDefault();
                if (memberSession is null) return false;

                memberSession.IsAttended = true;
                memberSession.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<MemberSession>().Update(memberSession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}

