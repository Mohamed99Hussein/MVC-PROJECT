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
    internal class MemberShipConfigurations : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            
            #region Plan - MemberShip
            builder.HasOne(MS => MS.Plan)
                .WithMany(P => P.MemberShips)
                .HasForeignKey(MS => MS.PlanId);
            #endregion
            #region Member - MemberShip
            builder.HasOne(MS => MS.Member)
                .WithMany(M => M.MemberShips)
                .HasForeignKey(MS => MS.MemberId);
            #endregion

            builder.Property(MS => MS.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");

            builder.HasKey(MS => new { MS.MemberId, MS.PlanId });

            builder.Ignore(MS => MS.Id);

        }
    }
}
