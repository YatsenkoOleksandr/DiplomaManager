using DiplomaManager.BLL.DTOs.TeacherDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefenseDateTeachersViewModel
    {
        public int PredefensePeriodId { get; set; }

        public int PredefenseDateId { get; set; }

        public int OldTeacherId { get; set; }

        public Dictionary<int, string> Teachers { get; set; }
    }
}
