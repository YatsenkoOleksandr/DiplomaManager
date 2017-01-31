using System.Collections.Generic;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.StudentEntities;

namespace DiplomaManager.BLL.DTOs.StudentDTOs
{
    public class DegreeDTO
    {
        public int Id
        { get; set; }

        public List<DegreeName> DegreeNames
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }
    }
}
