using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefensePeriodViewModel
    {
        [Display(Name="Дата начала периода")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "Дата окончания периода")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinishTime { get; set; }

        [Display(Name = "Время, отводимое студенту на предзащиту (в минутах)")]
        [Range(1, 120, ErrorMessage = "Предзащита должна длиться до 2-х часов")]
        public int StudentTime { get; set; }

        [Display(Name = "Образовательный уровень")]
        public int DegreeId { get; set; }

        [Display(Name = "Год выпуска")]
        public int GraduationYear { get; set; }
    }
}
