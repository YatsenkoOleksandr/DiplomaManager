using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefenseDateViewModel
    {
        public int PredefensePeriodId { get; set; }

        [Display(Name = "Дата проведения предзащит")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Время начала проведения предзащит")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh'/'mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Время окончания проведения предзащит")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh'/'mm}", ApplyFormatInEditMode = true)]
        public DateTime FinishTime { get; set; }
    }
}
