using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data.Contexts
{
    public class GymSystemDBContext : DbContext
    {
        public GymSystemDBContext(DbContextOptions<GymSystemDBContext> options) : base(options)
        {

        }
        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   optionsBuilder.UseSqlServer("Server=.;Database=GymSystemDB;Trusted_Connection=True;TrustServerCertificate=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
