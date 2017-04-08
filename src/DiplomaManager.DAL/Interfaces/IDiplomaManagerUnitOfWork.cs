using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;

namespace DiplomaManager.DAL.Interfaces
{
    public interface IDiplomaManagerUnitOfWork : IUnitOfWork
    {
        IRepository<DevelopmentArea> DevelopmentAreas { get; }

        IRepository<Locale> Locales { get; }

        IRepository<User> Users { get; }

        IRepository<Admin> Admins { get; }

        IRepository<Teacher> Teachers { get; }

        IRepository<Student> Students { get; }

        IRepository<Project> Projects { get; }

        IRepository<ProjectTitle> ProjectTitles { get; }

        IRepository<Position> Positions { get; }

        IRepository<PositionName> PositionNames { get; }

        IRepository<PeopleName> PeopleNames { get; }

        IRepository<Degree> Degrees { get; }

        IRepository<DegreeName> DegreeNames { get; }

        IRepository<Capacity> Capacities { get; }

        IRepository<Group> Groups { get; }
    }
}
