using System;

namespace DiplomaManager.DAL.RequestEntities
{
    public class Capacity
    {
        public int ID
        { get; set; }

        public TeacherEntities.Teacher Teacher
        { get; set; }

        public StudentEntities.Degree Degree
        { get; set; }

        public int Count
        { get; set; }

        public int AcceptedCount
        { get; set; }

        public DateTime StudyingYear
        { get; set; }
    }
}
