using AutoMapper;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.Areas.Admin.ViewModels;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;

namespace DiplomaManager.Modules
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                // Map entities to DTO's

                config.CreateMap<DevelopmentArea, DevelopmentAreaDTO>();
                config.CreateMap<NameKind, NameKindDTO>();
                config.CreateMap<PeopleName, PeopleNameDTO>();
                config.CreateMap<DegreeName, DegreeNameDTO>();
                config.CreateMap<Degree, DegreeDTO>();
                config.CreateMap<Locale, LocaleDTO>();
                config.CreateMap<Group, GroupDTO>();
                config.CreateMap<User, UserDTO>();
                config.CreateMap<Student, StudentDTO>();
                config.CreateMap<Teacher, TeacherDTO>();
                config.CreateMap<ProjectTitle, ProjectTitleDTO>();
                config.CreateMap<Project, ProjectDTO>();
                config.CreateMap<Capacity, CapacityDTO>();
                config.CreateMap<PredefensePeriod, PredefensePeriodDTO>();
                config.CreateMap<PredefenseDate, PredefenseDateDTO>();
                config.CreateMap<Predefense, PredefenseDTO>();
                config.CreateMap<PredefenseTeacherCapacity, PredefenseTeacherCapacityDTO>();
                config.CreateMap<Appointment, AppointmentDTO>();


                // Map DTO to ViewModels
                config.CreateMap<ProjectDTO, ProjectShortInfo>()
                   .ForMember(
                       d => d.Id,
                       opt => opt.MapFrom(s => s.Id))
                   .ForMember(
                       d => d.Acceptance,
                       opt => opt.MapFrom(s => s.AcceptanceToString()))
                   .ForMember(
                       d => d.CreationalDate,
                       opt => opt.MapFrom(s => s.CreationDate.ToString()))
                   .ForMember(
                       d => d.Degree,
                       opt => opt.MapFrom(s => s.DegreeToString(193)))
                   .ForMember(
                       d => d.DegreeId,
                       opt => opt.MapFrom(s => s.Student.Group.DegreeId))
                   .ForMember(
                       d => d.GraduationYear,
                       opt => opt.MapFrom(s => s.Student.Group.GraduationYear))
                   .ForMember(
                       d => d.Group,
                       opt => opt.MapFrom(s => s.Student.Group.Name))
                   .ForMember(
                       d => d.PracticeJournalPassDate,
                       opt => opt.MapFrom(s => s.PracticeJournalPassDateToString()))
                   .ForMember(
                       d => d.Student,
                       opt => opt.MapFrom(s => s.Student.GetFullName(1)))
                   .ForMember(
                       d => d.Teacher,
                       opt => opt.MapFrom(s => s.Teacher.GetFullName(193)))
                   .ForMember(
                       d => d.Title,
                       opt => opt.MapFrom(s => s.TitleToString(1)));
            });
        }
    }
}
