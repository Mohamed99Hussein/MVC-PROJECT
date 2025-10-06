using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.Configurations
{
    internal class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {

            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint("CK_Session_EndTime_After_StartTime", "EndTime > StartTime");
                TB.HasCheckConstraint("SessionCapacity_check", "Capacity Between 1 and 25");

            });


            #region Session - Category

            builder.HasOne(S => S.Category)
                .WithMany(C => C.Sessions)
                .HasForeignKey(S => S.CategoryId);
            #endregion

            #region Session - Trainer

            builder.HasOne(S => S.SessionTrainer)
                .WithMany(T => T.TrainerSessions)
                .HasForeignKey(S => S.TrainerId);
            #endregion

        }
    }
}
