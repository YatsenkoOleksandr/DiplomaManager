using System.Collections.Generic;
using AutoMapper;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Interfaces;

namespace DiplomaManager.BLL.Services
{
    public class RequestService : IRequestService
    {
        private IUnitOfWork Database { get; }

        public RequestService(IUnitOfWork uow)
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
            return Mapper.Map<IEnumerable<DevelopmentArea>, List<DevelopmentAreaDTO>>(Database.DevelopmentAreas.GetAll());
        }

        public void AddDevelopmentArea(DevelopmentAreaDTO developmentArea)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentArea>());
            Database.DevelopmentAreas.Create(Mapper.Map<DevelopmentAreaDTO, DevelopmentArea>(developmentArea));
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
            Database.DevelopmentAreas.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
