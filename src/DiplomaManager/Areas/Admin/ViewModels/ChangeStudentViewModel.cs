using DiplomaManager.BLL.DTOs.StudentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class ChangeStudentViewModel
    {
        public int OldStudentId { get; set; }

        public int StudentId { get; set; }

        public int PredefenseId { get; set; }

        public int PredefensePeriodId { get; set; }

        public Dictionary<int, string> Students { get; set; }
    }
}
