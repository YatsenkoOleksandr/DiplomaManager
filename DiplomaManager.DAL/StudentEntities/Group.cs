using System.Collections.Generic;

namespace DiplomaManager.DAL.StudentEntities
{
    public class Group
    {
        public int Id
        { get; set; }

        public string Name
        { get; set; }

        public int DegreeId { get; set; }
        public Degree Degree
        { get; set; }

        public List<Student> Students
        { get; set; }
    }
}
