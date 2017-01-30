using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using DiplomaManager.DAL.Entities.EventsEntities;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;

namespace DiplomaManager.DAL.EF
{
    public class DiplomaManagerContext : DbContext
    {
        //User
        public DbSet<Locale> Locales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FirstName> FirstNames { get; set; }
        public DbSet<LastName> LastNames { get; set; }
        public DbSet<Patronymic> Patronymics { get; set; }

        //Student
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<DegreeName> DegreeNames { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        //Teacher
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionName> PositionNames { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        //Project
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTitle> ProjectTitles { get; set; }

        //Request
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<DevelopmentArea> DevelopmentAreas { get; set; }

        //Event
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Defense> Defenses { get; set; }
        public DbSet<UndergraduateDefense> UndergraduateDefenses { get; set; }

        public DiplomaManagerContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                        .HasMany(t => t.DevelopmentAreas)
                        .WithMany(da => da.Teachers)
                        .Map(i =>
                        {
                            i.MapLeftKey("DevelopmentAreaId");
                            i.MapRightKey("TeacherId");
                            i.ToTable("Interest");
                        });

            modelBuilder.Entity<Defense>()
                .HasRequired(d => d.Student)
                .WithOptional(s => s.Defense);
        }
    }
}
