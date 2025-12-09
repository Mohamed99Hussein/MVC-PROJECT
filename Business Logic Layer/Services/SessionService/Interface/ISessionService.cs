using Business_Logic_Layer.ViewModels.SessionViewModels;
using Data_Access_Layer.Unit_Of_Work.Interface;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.SessionService.Interface
{
    public interface ISessionService 
    {
        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int SessionId);

        bool CreateSession(CreateSessionViewModel createdSessionViewModel);

        UpdateSessionViewModel? GetSessionForUpdate(int SessionId);

        bool UpdateSession(int SessionId, UpdateSessionViewModel updatedSessionViewModel);

        bool DeleteSession(int SessionId);

        IEnumerable<TrainerToSelectViewModel> GetAllTrainersForDropDownList();

        IEnumerable<CategoryToSelectViewModel> GetAllCategoriesForDropDownList();


    }
}
