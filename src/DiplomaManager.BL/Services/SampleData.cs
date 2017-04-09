using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static void Initialize(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IDiplomaManagerUnitOfWork>();

                //Add Locales
                if (uow.Locales.IsEmpty())
                {
                    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                          .Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
                    foreach (var culture in cultures)
                    {
                        uow.Locales.Add(new Locale { Name = culture.Name, NativeName = culture.NativeName });
                    }
                    uow.Save();
                }

                //Add DevelopmentAreas
                if (uow.DevelopmentAreas.IsEmpty())
                {
                    uow.DevelopmentAreas.Add(new DevelopmentArea { Name = "Искусственный интеллект" });
                    uow.DevelopmentAreas.Add(new DevelopmentArea { Name = "Веб-разработка" });

                    uow.Save();
                }

                var admin = new Admin
                {
                    Login = "admin",
                    Password = "admin",
                    Email = "admin@mail.ru",
                    StatusCreationDate = DateTime.Now
                };

                //Add Admin User
                if (uow.Admins.IsEmpty())
                {
                    admin = new Admin
                    {
                        Login = "admin",
                        Password = "admin",
                        Email = "admin@mail.ru",
                        StatusCreationDate = DateTime.Now
                    };
                    uow.Admins.Add(admin);

                    uow.Save();

                    uow.PeopleNames.Add(new PeopleName
                    {
                        CreationDate = DateTime.Now,
                        LocaleId = 193,
                        NameKind = NameKind.LastName,
                        Name = "Телешев",
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
                        LocaleId = 1,
                        PositionId = 1
                    });

                    uow.Positions.Add(new Position());
                    uow.PositionNames.Add(new PositionName
                    {
                        Name = "Профессор",
                        LocaleId = 1,
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
                            LocaleId = 193,
                            Name = "Андрей",
                            NameKind = NameKind.FirstName,
                            Users = new List<User> { teacher, admin }
                        });

                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 53,
                            Name = "Andrew",
                            NameKind = NameKind.FirstName,
                            Users = new List<User> { teacher, admin }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 193,
                            Name = "Рыбкин",
                            NameKind = NameKind.LastName,
                            Users = new List<User> { teacher }
                        });

                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 53,
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
                            LocaleId = 193,
                            Name = "Павел",
                            NameKind = NameKind.FirstName,
                            Users = new List<User> { luchshev }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 193,
                            Name = "Лучшев",
                            NameKind = NameKind.LastName,
                            Users = new List<User> { luchshev }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 53,
                            Name = "Aleksandrovich",
                            NameKind = NameKind.Patronymic,
                            Users = new List<User> { admin, teacher, luchshev }
                        });
                    uow.PeopleNames.Add(
                        new PeopleName
                        {
                            CreationDate = DateTime.Now,
                            LocaleId = 193,
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
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 1, LocaleId = 193, Name = "Бакалавр"});
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 1, LocaleId = 53, Name = "Bachelor's degree" });

                    uow.Degrees.Add(new Degree());
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 2, LocaleId = 193, Name = "Магистр" });
                    uow.DegreeNames.Add(new DegreeName { DegreeId = 2, LocaleId = 53, Name = "Master's degree" });

                    uow.Save();
                }

                //Add Capacities
                if (uow.Capacities.IsEmpty())
                {
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 2,
                        Count = 6,
                        DegreeId = 1,
                        StudyingYear = DateTime.Now
                    });
                    uow.Capacities.Add(new Capacity
                    {
                        TeacherId = 2,
                        Count = 5,
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
                        Count = 3,
                        DegreeId = 2,
                        StudyingYear = DateTime.Now
                    });

                    uow.Save();
                }

                //Add Groups
                if (uow.Groups.IsEmpty())
                {
                    uow.Groups.Add(new Group { Name = "641П", DegreeId = 1 });
                    uow.Groups.Add(new Group { Name = "642П", DegreeId = 1 });
                    uow.Groups.Add(new Group { Name = "631ПСТ", DegreeId = 1 });
                    uow.Groups.Add(new Group { Name = "651П", DegreeId = 2 });

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
                                LocaleId = 193,
                                NameKind = NameKind.FirstName
                            },
                            new PeopleName
                            {
                                Name = "Гаврилов",
                                CreationDate = DateTime.Now,
                                LocaleId = 193,
                                NameKind = NameKind.LastName
                            },
                            new PeopleName
                            {
                                Name = "Андреевич",
                                CreationDate = DateTime.Now,
                                LocaleId = 193,
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
