using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.ViewModels.SessionViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string TrainerName { get; set; } = null!;

        public int Capacity { get; set; }

        public int AvailableSlots { get; set; }


        #region Computed Columns

        public string DisplayedDate => $"{StartDate:MMM,dd,yyyy}";
        
        public string DisplayedTime => $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";

        public TimeSpan Duration => EndDate - StartDate;

        public string Status
        {
            get
            {
                if (DateTime.Now < StartDate)
                {
                    return "Upcoming";
                }
                else if (DateTime.Now >= StartDate && DateTime.Now <= EndDate)
                {
                    return "Ongoing";
                }
                else
                {
                    return "Completed";
                }
            }

        }

        #endregion
    }
}
