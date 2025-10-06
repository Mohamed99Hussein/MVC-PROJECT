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
    internal class MemberConfigurations
                  : GymUserBaseConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        // new keyword to hide the inherited Configure method 
        // from GymUserBaseConfiguration<T> class.
        public new void Configure(EntityTypeBuilder<Member> builder)
        {

            #region properties
            // 1) To Accept the implemetation of GymUserBaseConfiguration<Member>
            // and override it if needed.
            // 2) Take care to call it before any other configuration
            // to avoid being overriden.
            base.Configure(builder);

            builder.Property(M => M.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");
            #endregion

          

            

        }
    }
}
