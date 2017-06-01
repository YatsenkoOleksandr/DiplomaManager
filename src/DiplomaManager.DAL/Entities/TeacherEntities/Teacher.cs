using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DiplomaManager.DAL.Entities.ProjectEntities;
using DiplomaManager.DAL.Entities.RequestEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Entities.PredefenseEntities;

namespace DiplomaManager.DAL.Entities.TeacherEntities
{
    [Table("Teachers")]
    public class Teacher : User
    {
        public int? PositionId { get; set; }
        public Position Position
        { get; set; }

        public List<DevelopmentArea> DevelopmentAreas
        { get; set; }

        public List<Capacity> Capacities
        { get; set; }

        public List<Project> Projects
        { get; set; }


        public List<Appointment> Appointments { get; set; }

        public List<PredefenseDate> PredefenseDates { get; set; }

        public List<PredefenseTeacherCapacity> PredefenseTeacherCapacities { get; set; }
    }
}
