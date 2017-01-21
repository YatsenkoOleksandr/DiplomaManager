using System.Collections.Generic;

namespace DiplomaManager.DAL.StudentEntities
{
    public class Student: UserEnitites.User
    {
        public Group Group
        { get; set; }

        public List<ProjectEntities.Project> Projects
        { get; set; }

        public List<EventsEntities.UndergraduateDefense> UndergraduateDefenses
        { get; set; }

        public EventsEntities.Defense Defense
        { get; set; }
    }
}
