using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class PredefenseResultsViewModel
    {
        public int Id { get; set; }

        public int PredefensePeriodId { get; set; }


        [Display(Name = "Статус прохождения предзащиты")]
        public bool? Passed { get; set; }

        [Range(0, 100, ErrorMessage = "Готовность выражается в процентах от 0 до 100")]
        [Display(Name = "Готовность ПО в процентах")]
        public int SoftwareReadiness { get; set; }

        [Range(0, 100, ErrorMessage = "Готовность выражается в процентах от 0 до 100")]
        [Display(Name = "Готовность пояснительной записки в процентах")]
        public int WritingReadiness { get; set; }

        [Range(0, 100, ErrorMessage = "Готовность выражается в процентах от 0 до 100")]
        [Display(Name = "Готовность презентации в процентах")]
        public int PresentationReadiness { get; set; }

        [Display(Name = "Доклад присутствует")]
        public bool ReportExist { get; set; }

        [Display(Name = "Раздел БЖД подписан")]
        public bool SafetySigned { get; set; }

        [Display(Name = "Экономический раздел подписан")]
        public bool EconomySigned { get; set; }

        [Display(Name = "Нормоконтроль пройден")]
        public bool ControlSigned { get; set; }
    }
}
