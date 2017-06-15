﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using DiplomaManager.DAL.Entities.PredefenseEntities;
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
        public DbSet<PeopleName> PeopleNames { get; set; }

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

        // Predefenses
        public DbSet<PredefensePeriod> PredefensePeriods { get; set; }
        public DbSet<PredefenseDate> PredefenseDates { get; set; }
        public DbSet<Predefense> Predefenses { get; set; }
        public DbSet<PredefenseTeacherCapacity> PredefenseTeacherCapacities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        public DiplomaManagerContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Map DevelopmentArea And Teacher entities with Many-To-Many relationship
            modelBuilder.Entity<Teacher>()
                        .HasMany(t => t.DevelopmentAreas)
                        .WithMany(da => da.Teachers)
                        .Map(i =>
                        {
                            i.MapLeftKey("DevelopmentAreaId");
                            i.MapRightKey("TeacherId");
                            i.ToTable("Interests");
                        });

            //Map User And PeopleName entities with Many-To-Many relationship
            modelBuilder.Entity<User>()
                        .HasMany(u => u.PeopleNames)
                        .WithMany(n => n.Users)
                        .Map(i =>
                        {
                            i.MapLeftKey("UserId");
                            i.MapRightKey("PeopleNameId");
                            i.ToTable("UserNames");
                        });
        }
    }
}