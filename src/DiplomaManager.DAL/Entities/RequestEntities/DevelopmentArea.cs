using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DiplomaManager.DAL.Entities.TeacherEntities;

namespace DiplomaManager.DAL.Entities.RequestEntities
{
    public class DevelopmentArea
    {
        public int Id
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name
        { get; set; }

        public List<Teacher> Teachers
        { get; set; }
    }
}
