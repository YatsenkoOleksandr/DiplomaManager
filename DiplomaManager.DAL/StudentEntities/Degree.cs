using System.Collections.Generic;

namespace DiplomaManager.DAL.StudentEntities
{
    public class Degree
    {
        public int ID
        { get; set; }

        public List<DegreeName> DegreeNames
        { get; set; }

        public List<Group> Groups
        { get; set; }

        public List<RequestEntities.Capacity> Capacities
        { get; set; }
    }
}
