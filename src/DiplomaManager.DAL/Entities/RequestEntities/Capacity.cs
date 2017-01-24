using System;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.TeacherEntities;

namespace DiplomaManager.DAL.Entities.RequestEntities
{
    public class Capacity
    {
        public int Id
        { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher
        { get; set; }

        public int DegreeId { get; set; }
        public Degree Degree
        { get; set; }

        public int Count
        { get; set; }

        public int AcceptedCount
        { get; set; }

        public DateTime StudyingYear
        { get; set; }
    }
}
