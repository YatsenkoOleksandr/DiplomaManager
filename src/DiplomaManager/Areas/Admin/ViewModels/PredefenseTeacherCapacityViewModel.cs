using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefenseTeacherCapacityViewModel
    {
        public int TeacherId { get; set; }

        public int PredefensePeriodId { get; set; }

        [Range(1, 5, ErrorMessage ="Максимальное количество посещений преподавателем - 5 дней")]
        [Display(Name = "Максимальное количество посещений преподавателем")]
        public int Total { get; set; }
    }
}
