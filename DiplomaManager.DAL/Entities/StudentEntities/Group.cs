using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.StudentEntities
{
    public class Group
    {
        public int Id
        { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name
        { get; set; }

        public int DegreeId { get; set; }
        public Degree Degree
        { get; set; }

        public List<Student> Students
        { get; set; }
    }
}
