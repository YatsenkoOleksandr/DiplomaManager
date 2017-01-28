using System;
using Autofac;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.SharedEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.BLL.Services
{
    public static class SampleData
    {
        public static void Initialize(IContainer container)
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IDiplomaManagerUnitOfWork>();

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
                }

                //Add Locales
                if (uow.Locales.IsEmpty())
                {
                    uow.Locales.Add(new Locale { Name = "English", NativeName = "English" });
                    uow.Locales.Add(new Locale { Name = "Russian", NativeName = "Русский" });
                    uow.Locales.Add(new Locale { Name = "Ukrainian", NativeName = "Українська" });
                    uow.Save();
                }

                //Add Positions
                if (uow.Positions.IsEmpty())
                {
                    
                }

                //Add Teachers
                if (uow.Teachers.IsEmpty())
                {
                    uow.Teachers.Add(new Teacher { Login = "t1", Password = "123"});
                }
            }
        }
    }
}
