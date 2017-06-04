using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefensePeriodTeachers
    {
        public int PredefensePeriodId { get; set; }

        public IEnumerable<PredefenseTeacherCapacityDTO> PeriodTeachers { get; set; }
    }
}
