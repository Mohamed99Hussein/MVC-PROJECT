using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = null!;

        public int Capacity { get; set; }

        // Start and End time of the session
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        #region Relationship Session - Category

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        #endregion

        #region Relationship Session - Trainer

        public int TrainerId { get; set; }

        public Trainer SessionTrainer { get; set; } = null!;


        #endregion

        #region Relationship Session - MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
        #endregion


    }
}
