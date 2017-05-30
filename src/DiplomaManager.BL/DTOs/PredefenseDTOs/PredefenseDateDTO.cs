using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.DTOs.PredefenseDTOs
{
    public class PredefenseDateDTO
    {
        public int Id { get; set; }

        public int PredefensePeriodId { get; set; }       

        public DateTime Date { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime FinishTime { get; set; }

        public List<PredefenseDTO> Predefenses { get; set; }
    }
}
