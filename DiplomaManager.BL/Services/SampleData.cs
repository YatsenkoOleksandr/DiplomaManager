using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using DiplomaManager.DAL.Entities.RequestEntities;
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
                if (uow.DevelopmentAreas.IsEmpty())
                {
                    uow.DevelopmentAreas.Create(new DevelopmentArea { Name = "Искусственный интеллект" });
                    uow.DevelopmentAreas.Create(new DevelopmentArea { Name = "Веб-разработка" });
                    uow.Save();
                }
            }
        }
    }
}
