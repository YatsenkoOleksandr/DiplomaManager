using AutoMapper;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            });                
        }
    }
}
