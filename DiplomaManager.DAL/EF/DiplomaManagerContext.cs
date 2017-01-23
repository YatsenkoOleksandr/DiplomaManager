using DiplomaManager.DAL.Entities.EventsEntities;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using Microsoft.EntityFrameworkCore;

namespace DiplomaManager.DAL.EF
{
    public class DiplomaManagerContext : DbContext
    {
        //User
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
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionName> PositionNames { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        //Project
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTitle> ProjectTitles { get; set; }

        //Request
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<DevelopmentArea> DevelopmentAreas { get; set; }
        public DbSet<Interest> Interests { get; set; }

        //Event
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Defense> Defenses { get; set; }
        public DbSet<UndergraduateDefense> UndergraduateDefenses { get; set; }

        /*public ApplicationContext()
        {

        }*/

        public DiplomaManagerContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interest>()
                .HasKey(i => new { i.DevelopmentAreaId, i.TeacherId });

            modelBuilder.Entity<Interest>()
                .HasOne(i => i.Teacher)
                .WithMany(t => t.Interests)
                .HasForeignKey(i => i.TeacherId);

            modelBuilder.Entity<Interest>()
                .HasOne(i => i.DevelopmentArea)
                .WithMany(da => da.Interests)
                .HasForeignKey(i => i.DevelopmentAreaId);
        }
    }
}
