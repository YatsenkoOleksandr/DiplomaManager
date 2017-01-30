using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.BLL.Services
{
    public class RequestService : IRequestService
    {
        private IDiplomaManagerUnitOfWork Database { get; }

        public RequestService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public DevelopmentAreaDTO GetDevelopmentArea(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentArea, DevelopmentAreaDTO>());
            return Mapper.Map<DevelopmentArea, DevelopmentAreaDTO>(Database.DevelopmentAreas.Get(id));
        }

        public IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentArea, DevelopmentAreaDTO>());
            var das = Database.DevelopmentAreas.Get();
            return Mapper.Map<IEnumerable<DevelopmentArea>, List<DevelopmentAreaDTO>>(das);
        }

        public void AddDevelopmentArea(DevelopmentAreaDTO developmentArea)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentArea>());
            Database.DevelopmentAreas.Add(Mapper.Map<DevelopmentAreaDTO, DevelopmentArea>(developmentArea));
            Database.Save();
        }

        public void UpdateDevelopmentArea(DevelopmentAreaDTO developmentArea)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentArea>());
            Database.DevelopmentAreas.Update(Mapper.Map<DevelopmentAreaDTO, DevelopmentArea>(developmentArea));
            Database.Save();
        }

        public void DeleteDevelopmentArea(int id)
        {
            Database.DevelopmentAreas.Remove(id);
            Database.Save();
        }

        public IEnumerable<TeacherDTO> GetTeachers(string cultureName = null)
        {
            List<Teacher> teachers;
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                teachers = Database.Teachers.Get(t => !(t is Admin)).ToList();

                Database.FirstNames.Get(
                    f => f.Locale.Name == cultureName, new [] { "Locale" });
                Database.LastNames.Get(
                    l => l.Locale.Name == cultureName, new[] { "Locale" });
                Database.Patronymics.Get(
                    p => p.Locale.Name == cultureName, new[] { "Locale" });
            }
            else
            {
                teachers = Database.Teachers.Get(new [] { "FirstNames", "LastNames", "Patronymics" }).ToList();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Teacher, TeacherDTO>());
            Mapper.Initialize(cfg => cfg.CreateMap<PeopleName, PeopleNameDTO>());

            var teacherDtos = Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers);
            return teacherDtos;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
