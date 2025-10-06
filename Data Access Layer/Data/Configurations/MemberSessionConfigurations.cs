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
    internal class MemberSessionConfigurations : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
           
           builder.Ignore(MS => MS.Id);
            builder.Property(MS => MS.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(MS => new { MS.MemberId, MS.SessionId });

            #region Member - MemberSession
            builder.HasOne(MS => MS.Member)
                .WithMany(M => M.MemberSessions)
                .HasForeignKey(MS => MS.MemberId);
            #endregion

            #region Session - MemberSession
            builder.HasOne(MS => MS.Session)
                .WithMany(S => S.MemberSessions)
                .HasForeignKey(MS => MS.SessionId);
            #endregion

        }
    }
}
