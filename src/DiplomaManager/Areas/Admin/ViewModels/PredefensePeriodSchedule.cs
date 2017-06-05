using DiplomaManager.BLL.Extensions.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefensePeriodSchedule
    {
        public int PredefensePeriodId { get; set; }

        public IEnumerable<PredefenseSchedule> PredefenseSchedule { get; set; }
    }
}
