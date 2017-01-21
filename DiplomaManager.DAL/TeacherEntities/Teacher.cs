using System.Collections.Generic;

namespace DiplomaManager.DAL.TeacherEntities
{
    public class Teacher: UserEnitites.User
    {
        public Position Position
        { get; set; }

        public List<RequestEntities.Capacity> Capacities
        { get; set; }

        public List<ProjectEntities.Project> Projects
        { get; set; }
    }
}
