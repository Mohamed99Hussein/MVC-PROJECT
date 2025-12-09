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
    internal class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(P => P.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(P => P.Price)
                .HasColumnType("decimal(10,2)");

            builder.Property(P => P.Description)
                .HasColumnType("varchar")
                .HasMaxLength(200);

            builder.ToTable(TB =>
            TB.HasCheckConstraint("Plan_Duration_check", "DurationDays Between 1 and 365"));

        }
    }
}
