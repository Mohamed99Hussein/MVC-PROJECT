using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.Configurations
{
    internal class HealthRecordConfigurations : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<HealthRecord> builder)
        {
           builder.HasKey(hr => hr.Id);

            builder.ToTable("Members");
            builder.Ignore(hr => hr.CreatedAt);
            builder.Ignore(hr => hr.UpdatedAt);

            // 1-1 relationship with Member [Shared p.k ]

            builder.HasOne<Member>()
                   .WithOne(m => m.HealthRecord)
                   .HasForeignKey<HealthRecord>(hr => hr.Id);
                  
        }
    }
}
