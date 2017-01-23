using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.UserEnitites;

namespace DiplomaManager.DAL.Entities.TeacherEntities
{
    [Table("Teachers")]
    public class Teacher : User
    {
        public int PositionId { get; set; }
        public Position Position
        { get; set; }

        //public List<Interest> Interests { get; set; }

        public List<DevelopmentArea> DevelopmentAreas
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }

        public List<Project> Projects
        { get; set; }
    }
}
