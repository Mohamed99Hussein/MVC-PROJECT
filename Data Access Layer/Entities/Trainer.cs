using Data_Access_Layer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    internal class Trainer : GymUser
    {
        // HireDate == CreatedAt from BaseEntity
        public Specialities Specialities { get; set; }


        #region Relationship Trainer - Session
        public ICollection<Session> TrainerSessions { get; set; } = new HashSet<Session>();

        #endregion
    }
}
