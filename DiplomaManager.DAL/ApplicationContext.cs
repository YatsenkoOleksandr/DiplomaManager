using DiplomaManager.DAL.EventsEntities;
using DiplomaManager.DAL.ProjectEntities;
using DiplomaManager.DAL.RequestEntities;
using DiplomaManager.DAL.StudentEntities;
using DiplomaManager.DAL.TeacherEntities;
using DiplomaManager.DAL.UserEnitites;
using Microsoft.EntityFrameworkCore;

namespace DiplomaManager.DAL
{
    class ApplicationContext : DbContext
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

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=diplomamanagerdb;Trusted_Connection=True;");
        }
    }
}
