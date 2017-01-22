using System;
using DiplomaManager.DAL.Entities.StudentEntities;

namespace DiplomaManager.DAL.Entities.EventsEntities
{
    public class Defense
    {
        public int Id
        { get; set; }

        public int StudentId { get; set; }
        public Student Student
        { get; set; }

        public DateTime Date
        { get; set; }

        public int Estimate
        { get; set; }
    }
}
