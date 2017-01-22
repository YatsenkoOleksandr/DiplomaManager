using System.Collections.Generic;
using DiplomaManager.DAL.Entities.RequestEntities;

namespace DiplomaManager.DAL.Entities.StudentEntities
{
    public class Degree
    {
        public int Id
        { get; set; }

        public List<DegreeName> DegreeNames
        { get; set; }

        public List<Group> Groups
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }
    }
}
