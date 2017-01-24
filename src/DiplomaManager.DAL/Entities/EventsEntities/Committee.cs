using System;
using System.Collections.Generic;

namespace DiplomaManager.DAL.Entities.EventsEntities
{
    public class Committee
    {
        public int Id
        { get; set; }

        public List<Appointment> Appointments
        { get; set; }

        public List<UndergraduateDefense> UndergraduateDefenses
        { get; set; }

        public DateTime Date
        { get; set; }

        public DateTime StartTime
        { get; set; }
    }
}
