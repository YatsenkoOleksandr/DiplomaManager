using System;

namespace DiplomaManager.DAL.EventsEntities
{
    public class Defense
    {
        public int ID
        { get; set; }

        public StudentEntities.Student Student
        { get; set; }

        public DateTime Date
        { get; set; }

        public int Estimate
        { get; set; }
    }
}
