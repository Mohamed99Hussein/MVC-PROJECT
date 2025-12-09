using Business_Logic_Layer.ViewModels.SessionViewModels;
using Data_Access_Layer.Unit_Of_Work.Interface;
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
       

    }
}
