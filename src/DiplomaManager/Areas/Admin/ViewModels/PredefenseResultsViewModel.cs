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
        public PredefenseStatus Passed { get; set; }

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

        public static Dictionary<PredefenseStatus, string> PredefensesStatus
        {
            get;
            private set;
        }

        static PredefenseResultsViewModel()
        {
            PredefensesStatus = new Dictionary<PredefenseStatus, string>();
            PredefensesStatus.Add(PredefenseStatus.NotPassed, "Предзащита не пройдена");
            PredefensesStatus.Add(PredefenseStatus.Failed, "Студент не допущен к защите");
            PredefensesStatus.Add(PredefenseStatus.Passed, "Студент допущен к защите");
        }

        public static PredefenseStatus ConvertToPredefenseStatus(bool? passed)
        {
            PredefenseStatus status = PredefenseStatus.NotPassed;
            switch (passed)
            {
                case true:
                    status = PredefenseStatus.Passed;
                    break;
                case false:
                    status = PredefenseStatus.Failed;
                    break;
            }
            return status;
        }

        public static bool? ConvertPredefenseStatus(PredefenseStatus status)
        {
            bool? passed = null;
            switch (status)
            {
                case PredefenseStatus.Failed:
                    passed = false;
                    break;
                case PredefenseStatus.Passed:
                    passed = true;
                    break;
            }
            return passed;
        }

    }
}
