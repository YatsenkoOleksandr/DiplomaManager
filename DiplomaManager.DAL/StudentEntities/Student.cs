using System.Collections.Generic;
using DiplomaManager.DAL.EventsEntities;
using DiplomaManager.DAL.ProjectEntities;
using DiplomaManager.DAL.UserEnitites;

namespace DiplomaManager.DAL.StudentEntities
{
    public class Student : User
    {
        public int GroupId { get; set; }
        public Group Group
        { get; set; }

        public List<Project> Projects
        { get; set; }

        public List<UndergraduateDefense> UndergraduateDefenses
        { get; set; }

        public int DefenseId { get; set; }
        public Defense Defense
        { get; set; }
    }
}
