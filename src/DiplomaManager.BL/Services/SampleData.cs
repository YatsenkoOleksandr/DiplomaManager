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

                //Add Admin User
                if (uow.Admins.IsEmpty())
                {
                    uow.Admins.Add(new Admin
                    {
                        Login = "admin",
                        Password = "admin",
                        Email = "admin@mail.ru",
                        StatusCreationDate = DateTime.Now
                    });

                    uow.Save();

                    uow.FirstNames.Add(
                        new FirstName { CreationDate = DateTime.Now, LocaleId = 193, Name = "Андрей", UserId = 1 });
                    uow.LastNames.Add(
                        new LastName { CreationDate = DateTime.Now, LocaleId = 193, Name = "Телешев", UserId = 1 });
                    uow.Patronymics.Add(
                        new Patronymic { CreationDate = DateTime.Now, LocaleId = 193, Name = "Максимович", UserId = 1 });

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
                    uow.Teachers.Add(new Teacher
                    {
                        Login = "t1",
                        Password = "123",
                        Email = "te@te.ru", 
                        PositionId = 2,
                        StatusCreationDate = DateTime.Now,
                        DevelopmentAreas = uow.DevelopmentAreas.Get(da => da.Id == 1).ToList()
                    });

                    uow.Save();

                    uow.FirstNames.Add(
                        new FirstName { CreationDate = DateTime.Now, LocaleId = 193, Name = "Андрей", UserId = 2 });
                    uow.FirstNames.Add(
                        new FirstName { CreationDate = DateTime.Now, LocaleId = 53, Name = "Andrew", UserId = 2 });

                    uow.LastNames.Add(
                        new LastName { CreationDate = DateTime.Now, LocaleId = 193, Name = "Рыбкин", UserId = 2 });
                    uow.LastNames.Add(
                        new LastName { CreationDate = DateTime.Now, LocaleId = 53, Name = "Rybkin", UserId = 2 });

                    uow.Patronymics.Add(
                        new Patronymic { CreationDate = DateTime.Now, LocaleId = 193, Name = "Александрович", UserId = 2 });
                    uow.Patronymics.Add(
                        new Patronymic { CreationDate = DateTime.Now, LocaleId = 53, Name = "Aleksandrovich", UserId = 2 });

                    uow.Save();

                    uow.Teachers.Add(new Teacher
                    {
                        Login = "t2",
                        Password = "123",
                        Email = "lu@lu.ru",
                        PositionId = 1,
                        StatusCreationDate = DateTime.Now,
                        DevelopmentAreas = uow.DevelopmentAreas.Get(da => da.Id == 1 || da.Id == 2).ToList()
                    });

                    uow.Save();

                    uow.FirstNames.Add(
                        new FirstName { CreationDate = DateTime.Now, LocaleId = 193, Name = "Павел", UserId = 3 });
                    uow.LastNames.Add(
                        new LastName { CreationDate = DateTime.Now, LocaleId = 193, Name = "Лучшев", UserId = 3 });
                    uow.Patronymics.Add(
                        new Patronymic { CreationDate = DateTime.Now, LocaleId = 193, Name = "Александрович", UserId = 3 });

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

                    uow.Save();
                }
            }
        }
    }
}
