using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;

namespace DiplomaManager.BLL.Services
{
    public static class SampleData
    {
        private static readonly IEnumerable<Locale> Locales = new[]
        {
            new Locale { Id = 1, Name = "", NativeName = "Invariant Language" },
            new Locale { Id = 2, Name = "en", NativeName = "English" },
            new Locale { Id = 3, Name = "ru", NativeName = "Русский" },
            new Locale { Id = 4, Name = "uk", NativeName = "Українська" }
        };

        public static void Initialize(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IDiplomaManagerUnitOfWork>();

                //Add Locales
                if (uow.Locales.IsEmpty())
                {
                    uow.Locales.AddRange(Locales, true);
                }

                //Add DevelopmentAreas
                if (uow.DevelopmentAreas.IsEmpty())
                {
                    uow.DevelopmentAreas.Add(new DevelopmentArea { Name = "Искусственный интеллект" });
                    uow.DevelopmentAreas.Add(new DevelopmentArea { Name = "Веб-разработка" });

                    uow.Save();
                }

                Admin admin = new Admin
                {
                    Login = "admin",
                    Password = "admin",
                    Email = "admin@mail.ru",
                    StatusCreationDate = DateTime.Now
                };

                //Add Admin User
                if (uow.Admins.IsEmpty())
                {
                    uow.Admins.Add(admin);

                    uow.Save();

                    uow.PeopleNames.Add(new PeopleName
                    {
                        CreationDate = DateTime.Now,
                        LocaleId = 3,
                        NameKind = NameKind.LastName,
                        Name = "Шевелев",
                        Users = new List<User> { admin }
                    });

                    uow.Save();
                }

                //Add Positions
                if (uow.Positions.IsEmpty() && uow.PositionNames.IsEmpty())
                {
                    uow.Positions.Add(new Position());
                    uow.PositionNames.Add(new PositionName
                    {
                        Name = "Кандидат технических наук",
                        LocaleId = 3,
                        PositionId = 1
                    });

                    uow.Positions.Add(new Position());
                    uow.PositionNames.Add(new PositionName
                    {
                        Name = "Профессор",
                        LocaleId = 3,
                        PositionId = 2
                    });

                    uow.Save();
                }

                //Add Teachers
                if (uow.Teachers.IsEmpty(new FilterExpression<Teacher>(t => !(t is Admin))))
                {
                    var teacher = new Teacher
                    {
                        Login = "t1",
                        Password = "123",
                        Email = "te@te.ru",
                        PositionId = 2,
                        StatusCreationDate = DateTime.Now,
                        DevelopmentAreas = uow.DevelopmentAreas.Get(new FilterExpression<DevelopmentArea>(da => da.Id == 1)).ToList()
                    };
                    uow.Teachers.Add(teacher);

                    uow.Save();

                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 3,
                            Name = "Игорь",
                            NameKind = NameKind.FirstName,
                            Users = new List<User> { teacher, admin }
                        });

                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 2,
                            Name = "Igor",
                            NameKind = NameKind.FirstName,
                            Users = new List<User> { teacher, admin }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 3,
                            Name = "Рыбкин",
                            NameKind = NameKind.LastName,
                            Users = new List<User> { teacher }
                        });

                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 2,
                            Name = "Rybkin",
                            NameKind = NameKind.LastName,
                            Users = new List<User> { teacher }
                        });

                    uow.Save();

                    var luchshev = new Teacher
                    {
                        Login = "t2",
                        Password = "123",
                        Email = "lu@lu.ru",
                        PositionId = 1,
                        StatusCreationDate = DateTime.Now,
                        DevelopmentAreas = uow.DevelopmentAreas.Get(new FilterExpression<DevelopmentArea>(da => da.Id == 1 || da.Id == 2)).ToList()
                    };
                    uow.Teachers.Add(luchshev);

                    uow.Save();

                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 3,
                            Name = "Павел",
                            NameKind = NameKind.FirstName,
                            Users = new List<User> { luchshev }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 3,
                            Name = "Лучшев",
                            NameKind = NameKind.LastName,
                            Users = new List<User> { luchshev }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 2,
                            Name = "Aleksandrovich",
                            NameKind = NameKind.Patronymic,
                            Users = new List<User> { admin, teacher, luchshev }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 3,
                            Name = "Александрович",
                            NameKind = NameKind.Patronymic,
                            Users = new List<User> { admin, teacher, luchshev }
                        });

                    uow.Save();
                }

                //Add Degrees
                if (uow.Degrees.IsEmpty() && uow.DegreeNames.IsEmpty())
                {
                    uow.Degrees.Add(new Degree());
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 1, LocaleId = 3, Name = "Бакалавр" });
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 1, LocaleId = 2, Name = "Bachelor's degree" });

                    uow.Degrees.Add(new Degree());
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 2, LocaleId = 3, Name = "Магистр" });
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 2, LocaleId = 2, Name = "Master's degree" });

                    uow.Save();
                }

                //Add Capacities
                if (uow.Capacities.IsEmpty())
                {
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 1,
                        Count = 10,
                        DegreeId = 1,
                        StudyingYear = DateTime.Now
                    });
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 1,
                        Count = 8,
                        DegreeId = 2,
                        StudyingYear = DateTime.Now
                    });
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 2,
                        Count = 12,
                        DegreeId = 1,
                        StudyingYear = DateTime.Now
                    });
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 2,
                        Count = 8,
                        DegreeId = 2,
                        StudyingYear = DateTime.Now
                    });
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 3,
                        Count = 8,
                        DegreeId = 1,
                        StudyingYear = DateTime.Now
                    });
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 3,
                        Count = 10,
                        DegreeId = 2,
                        StudyingYear = DateTime.Now
                    });

                    uow.Save();
                }

                //Add Groups
                if (uow.Groups.IsEmpty())
                {
                    uow.Groups.Add(new Group
                    {
                        Name = "641п",
                        DegreeId = 1,
                        GraduationYear = DateTime.Now.Year
                    });
                    uow.Groups.Add(new Group
                    {
                        Name = "642п",
                        DegreeId = 1,
                        GraduationYear = DateTime.Now.Year
                    });
                    uow.Groups.Add(new Group
                    {
                        Name = "631пст",
                        DegreeId = 1,
                        GraduationYear = DateTime.Now.Year
                    });

                    uow.Save();
                }

                //Add Student
                if (uow.Students.IsEmpty())
                {
                    uow.Students.Add(new Student
                    {
                        StatusCreationDate = DateTime.Now,
                        GroupId = 1,
                        PeopleNames = new List<PeopleName>
                        {
                            new PeopleName
                            {
                                Name = "Алексей",
                                CreationDate = DateTime.Now,
                                LocaleId = 3,
                                NameKind = NameKind.FirstName
                            },
                            new PeopleName
                            {
                                Name = "Гаврилов",
                                CreationDate = DateTime.Now,
                                LocaleId = 3,
                                NameKind = NameKind.LastName
                            },
                            new PeopleName
                            {
                                Name = "Андреевич",
                                CreationDate = DateTime.Now,
                                LocaleId = 3,
                                NameKind = NameKind.Patronymic
                            }
                        }
                    });

                    uow.Save();
                }
            }
        }
    }
}