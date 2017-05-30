using System;
using DiplomaManager.DAL.Entities.StudentEntities;
using System.Collections.Generic;

namespace DiplomaManager.DAL.Entities.PredefenseEntities
{
    public class PredefensePeriod
    {
        public int Id { get; set; }

        public int DegreeId { get; set; }
        public Degree Degree { get; set; }

        public int GraduationYear { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public TimeSpan PredefenseStudentTime { get; set; }

        public List<PredefenseDate> PredefenseDates { get; set; }
    }
}
