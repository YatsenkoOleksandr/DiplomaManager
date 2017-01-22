using System;
using System.Collections.Generic;
using DiplomaManager.DAL.EventsEntities;

namespace DiplomaManager.BL.EventsDTOs
{
    public class CommitteeDTO
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
