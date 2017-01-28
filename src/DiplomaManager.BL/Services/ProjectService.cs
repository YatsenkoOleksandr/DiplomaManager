using System.Collections.Generic;
using AutoMapper;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private IDiplomaManagerUnitOfWork Database { get; }

        public ProjectService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<TeacherDTO> GetTeachers()
        {
            var teachers = Database.Teachers.Get();
            Mapper.Initialize(cfg => cfg.CreateMap<Teacher, TeacherDTO>());
            var teacherDtos = Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers);
            return teacherDtos;
        }

        public IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas()
        {
            var das = Database.DevelopmentAreas.Get();
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentArea, DevelopmentAreaDTO>());
            var daDtos = Mapper.Map<IEnumerable<DevelopmentArea>, IEnumerable<DevelopmentAreaDTO>>(das);
            return daDtos;
        }
    }
}
