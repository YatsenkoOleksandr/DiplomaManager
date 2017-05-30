using DiplomaManager.DAL.Entities.TeacherEntities;
using System;
using System.Collections.Generic;

namespace DiplomaManager.DAL.Entities.PredefenseEntities
{
    public class PredefenseTeacherCapacity
    {
        public int Id { get; set; }

        public int PredefensePeriodId { get; set; }
        public PredefensePeriod PredefensePeriod { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int Total { get; set; }

        public int Count { get; set; }
    }
}
