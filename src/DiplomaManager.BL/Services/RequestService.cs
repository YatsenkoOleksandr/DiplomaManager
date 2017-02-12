using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.BLL.Services
{
    public class RequestService : IRequestService
    {
        private IDiplomaManagerUnitOfWork Database { get; }

        public RequestService(IDiplomaManagerUnitOfWork uow, ITransliterationService transliteration)
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
            var das = Database.DevelopmentAreas.Get();
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentArea, DevelopmentAreaDTO>());
            return Mapper.Map<IEnumerable<DevelopmentArea>, IEnumerable<DevelopmentAreaDTO>>(das);
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

        public IEnumerable<TeacherDTO> GetTeachers(int? daId = null, string cultureName = null)
        {
            List<Teacher> teachers;
            var filterExpressions = new List<FilterExpression<Teacher>>
            { new FilterExpression<Teacher>(t => !(t is Admin)) };
            var includePaths = new List<string>();
            if (daId != null)
            {
                includePaths.Add("DevelopmentAreas");
                filterExpressions.Add(new FilterExpression<Teacher>(
                    t => t.DevelopmentAreas.Any(da => da.Id == daId.Value)));
            }
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                teachers = Database.Teachers.Get(filters: filterExpressions.ToArray(), 
                    includePaths: includePaths.ToArray()).ToList();

                Database.FirstNames.Get(
                    f => f.Locale.Name == cultureName, new [] { "Locale" });
                Database.LastNames.Get(
                    l => l.Locale.Name == cultureName, new[] { "Locale" });
                Database.Patronymics.Get(
                    p => p.Locale.Name == cultureName, new[] { "Locale" });
            }
            else
            {
                includePaths.AddRange(new[] { "FirstNames", "LastNames", "Patronymics" });
                teachers = Database.Teachers.Get(filters: filterExpressions.ToArray(),
                    includePaths: includePaths.ToArray()).ToList();
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FirstName, FirstNameDTO>();
                cfg.CreateMap<LastName, LastNameDTO>();
                cfg.CreateMap<Patronymic, PatronymicDTO>();

                cfg.CreateMap<Teacher, TeacherDTO>();
            });

            var teacherDtos = Mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDTO>>(teachers);
            return teacherDtos;
        }

        public IEnumerable<DegreeDTO> GetDegrees(string cultureName = null)
        {
            List<Degree> degrees;
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                degrees = Database.Degrees.Get().ToList();
                Database.DegreeNames.Get(dn => dn.Locale.Name == cultureName, new[] { "Locale" });
            }
            else
            {
                degrees = Database.Degrees.Get(new[] { "DegreeNames" }).ToList();
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Degree, DegreeDTO>();
                cfg.CreateMap<DegreeName, DegreeNameDTO>();
                cfg.CreateMap<Capacity, CapacityDTO>();
            });

            var degreeDtos = Mapper.Map<IEnumerable<Degree>, IEnumerable<DegreeDTO>>(degrees);
            return degreeDtos;
        }

        public CapacityDTO GetCapacity(int degreeId, int teacherId)
        {
            var capacity = Database.Capacities
                .Get(c => c.DegreeId == degreeId && c.TeacherId == teacherId)
                .SingleOrDefault();
            Mapper.Initialize(cfg => cfg.CreateMap<Capacity, CapacityDTO>());
            var capacityDto = Mapper.Map<Capacity, CapacityDTO>(capacity);
            return capacityDto;
        }

        public void CreateDiplomaRequest(StudentDTO studentDto, int daId, int teacherId, int localeId, string title)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<FirstNameDTO, FirstName>();
                cfg.CreateMap<LastNameDTO, LastName>();
                cfg.CreateMap<PatronymicDTO, Patronymic>();

                cfg.CreateMap<StudentDTO, Student>();
            });

            var studentRes = CreateStudent(studentDto, localeId);

            var project = new Project
            {
                Student = studentRes,
                TeacherId = teacherId,
                CreationDate = DateTime.Now,
                DevelopmentAreaId = daId,
                Accepted = false
            };
            Database.Projects.Add(project);

            var projTitle = new ProjectTitle { Project = project, Title = title, CreationDate = DateTime.Now, LocaleId = localeId };
            Database.ProjectTitles.Add(projTitle);

            Database.Save();
        }

        private Student CreateStudent(StudentDTO studentDto, int localeId)
        {
            var studentRes = Database.Students.Get(s => s.Email == studentDto.Email).FirstOrDefault();
            if (studentRes == null)
            {
                var student = Mapper.Map<StudentDTO, Student>(studentDto);

                student.StatusCreationDate = DateTime.Now;
                student.GroupId = 1;
                student.Login =
                    $"{studentDto.GetLastName(localeId)}{studentDto.GetFirstName(localeId).Substring(0, 1).ToUpper()}{studentDto.GetPatronymic(localeId).Substring(0, 1).ToUpper()}";
                student.Password = CreateRandomPassword(8);

                Database.Students.Add(student);
                studentRes = student;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(studentRes.Login))
                {
                    studentRes.Login =
                        $"{studentDto.GetLastName(localeId)}{studentDto.GetFirstName(localeId).Substring(0, 1).ToUpper()}{studentDto.GetPatronymic(localeId).Substring(0, 1).ToUpper()}";
                    Database.Students.Update(studentRes);
                }
                if (string.IsNullOrWhiteSpace(studentRes.Password))
                {
                    studentRes.Password = CreateRandomPassword(8);
                    Database.Students.Update(studentRes);
                }
            }
            return studentRes;
        }

        private string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
