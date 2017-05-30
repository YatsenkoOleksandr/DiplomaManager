using DiplomaManager.BLL.DTOs.StudentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.DTOs.PredefenseDTOs
{
    public class PredefensePeriodDTO
    {
        public int Id { get; set; }

        public int DegreeId { get; set; }
        public DegreeDTO Degree { get; set; }

        public int GraduationYear { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public TimeSpan PredefenseStudentTime { get; set; }

        public List<PredefenseDateDTO> PredefenseDates { get; set; }

    }
}
