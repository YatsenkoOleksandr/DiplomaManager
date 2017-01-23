using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.RequestDTOs;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<DevelopmentAreaDTO> GetDevelopmentAreas();
        void AddDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void UpdateDevelopmentArea(DevelopmentAreaDTO developmentArea);
        void DeleteDevelopmentArea(int id);

        void Dispose();
    }
}
