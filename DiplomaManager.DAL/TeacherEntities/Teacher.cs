using System.Collections.Generic;
using DiplomaManager.DAL.ProjectEntities;
using DiplomaManager.DAL.RequestEntities;
using DiplomaManager.DAL.UserEnitites;

namespace DiplomaManager.DAL.TeacherEntities
{
    public class Teacher : User
    {
        public int PositionId { get; set; }
        public Position Position
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }

        public List<Project> Projects
        { get; set; }
    }
}
