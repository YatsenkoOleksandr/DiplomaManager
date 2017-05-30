using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.DAL.Entities.PredefenseEntities
{
    public class PredefenseDate
    {
        public int Id { get; set; }

        public int PredefensePeriodId { get; set; } 
        public PredefensePeriod PredefensePeriod { get; set; }

        public DateTime Date { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime FinishTime { get; set; }

        public List<Predefense> Predefenses { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}
