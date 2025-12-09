using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.Contexts
{
    public class GymSystemDBContext : IdentityDbContext<ApplicationUser>
    {
        public GymSystemDBContext(DbContextOptions<GymSystemDBContext> options) : base(options)
        {

        }
        

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           modelBuilder.Entity<ApplicationUser>(
                  mb=>
                  {
                      mb.Property(e=>e.FirstName)
                      .HasColumnType("varchar")
                      .HasMaxLength(50);

                      mb.Property(e=>e.LastName)
                       .HasColumnType("varchar")
                       .HasMaxLength(50);

                  });
        
        }

        #region DbSets

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<MemberShip> Memberships { get; set; }

        public DbSet<HealthRecord> HealthRecords { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<MemberSession> MemberSessions { get; set; }

        public DbSet<Category> Categories { get; set; }

        #endregion

    }
}
