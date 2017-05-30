using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Extensions.PredefenseService
{
    public class TeacherPredefensePeriod
    {
        public PredefensePeriodDTO PredefensePeriod { get; set; }

        public PredefenseTeacherCapacityDTO PredefenseCapacity { get; set; }
    }
}
