using Business_Logic_Layer.ViewModels.AnalyticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AnalyticsService.Interface
{
    public interface IAnalyticsService
    {
        AnalyticsViewModel GetAnalyticsData();

    }
}
