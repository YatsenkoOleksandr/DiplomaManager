using System;
using System.Linq;
using AutoMapper;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.BLL.Services
{
    public class UserService : IUserService
    {
        private IDiplomaManagerUnitOfWork Database { get; }

        public UserService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public T GetUser<T>(string login, string password)
            where T : UserDTO, new()
        {
            var type = typeof(T).Name;
            Mapper.Initialize(cfg => cfg.CreateMap<Admin, AdminDTO>());
            switch (type)
            {
                case nameof(AdminDTO):
                {
                    var user = Database.Admins.Get(u => u.Login == login && u.Password == password)
                        .SingleOrDefault();
                    Mapper.Initialize(cfg => cfg.CreateMap<Admin, AdminDTO>());
                    var userDto = Mapper.Map<Admin, AdminDTO>(user);
                    return (T) (object) userDto;
                }
                case nameof(TeacherDTO):
                {
                    var user = Database.Teachers.Get(u => u.Login == login && u.Password == password)
                        .SingleOrDefault();
                    Mapper.Initialize(cfg => cfg.CreateMap<Teacher, TeacherDTO>());
                    var userDto = Mapper.Map<Teacher, TeacherDTO>(user);
                    return (T) (object) userDto;
                }
                case nameof(StudentDTO):
                {
                    var user = Database.Students.Get(u => u.Login == login && u.Password == password)
                        .SingleOrDefault();
                    Mapper.Initialize(cfg => cfg.CreateMap<Student, StudentDTO>());
                    var userDto = Mapper.Map<Student, StudentDTO>(user);
                    return (T) (object) userDto;
                }
                default:
                    return null;
            }
        }
    }
}

