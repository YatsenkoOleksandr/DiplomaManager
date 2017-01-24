using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaManager.DAL.Entities.EventsEntities;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.UserEnitites;

namespace DiplomaManager.DAL.Entities.StudentEntities
{
    [Table("Students")]
    public class Student : User
    {
        public int GroupId { get; set; }
        public Group Group
        { get; set; }

        public List<Project> Projects
        { get; set; }

        public List<UndergraduateDefense> UndergraduateDefenses
        { get; set; }

        public Defense Defense
        { get; set; }
    }
}
