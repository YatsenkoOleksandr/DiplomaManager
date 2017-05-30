using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.DTOs.PredefenseDTOs
{
    public class PredefenseDTO
    {
        public int Id { get; set; }

        public int PredefenseDateId { get; set; }        

        public int? StudentId { get; set; }        

        public bool? Passed { get; set; }

        public int SoftwareReadiness { get; set; }

        public int WritingReadiness { get; set; }

        public int PresentationReadiness { get; set; }

        public bool ReportExist { get; set; }

        public bool SafetySigned { get; set; }

        public bool EconomySigned { get; set; }

        public bool ControlSigned { get; set; }
    }
}
