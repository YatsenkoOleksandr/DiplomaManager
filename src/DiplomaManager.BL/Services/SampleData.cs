using System;
using Autofac;
using DiplomaManager.DAL.Entities.RequestEntities;
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
                var uow = scope.Resolve<IUnitOfWork>();

                //Add DevelopmentAreas
                if (uow.DevelopmentAreas.IsEmpty())
                {
                    uow.DevelopmentAreas.Create(new DevelopmentArea { Name = "Искусственный интеллект" });
                    uow.DevelopmentAreas.Create(new DevelopmentArea { Name = "Веб-разработка" });
                    uow.Save();
                }

                //Add Admin User
                if (uow.Admins.IsEmpty())
                {
                    uow.Admins.Create(new Admin
                    {
                        Login = "admin",
                        Password = "admin",
                        Email = "admin@mail.ru",
                        StatusCreationDate = DateTime.Now
                    });
                    uow.Save();
                }
            }
        }
    }
}
