using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.Configurations
{
    internal class GymUserBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {

            #region Common Properties

            builder.Property(N => N.Name)
              .HasColumnType("varchar")
              .HasMaxLength(50);

            builder.Property(E => E.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

             builder.ToTable(TB=>
             TB.HasCheckConstraint("CK_Email_Format", "Email LIKE '%_@__%.__%'"));

            // Unique Non-Clustered Index on Email
            builder.HasIndex(E => E.Email).IsUnique();

            builder.Property(P => P.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(11);

            builder.ToTable(TB =>
            TB.HasCheckConstraint("CK_Phone_Format", "Phone LIKE '01[0125]________'"));

            // Unique Non-Clustered Index on Phone
            builder.HasIndex(P => P.Phone).IsUnique();

          

            builder.OwnsOne(A => A.Address, AB =>
            {
                AB.Property(A => A.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AB.Property(A => A.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);

                AB.Property(A => A.BuildingNumber)
                    .HasColumnName("BuildingNumber")
                    .HasColumnType("int");
            });





            #endregion


        }
    }
}
