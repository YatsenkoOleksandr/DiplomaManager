using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.TeacherDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Extensions.PredefenseService
{
    /// <summary>
    /// Class discribes day of predefense
    /// </summary>    
    public class PredefenseSchedule
    {
        /// <summary>
        /// Date with predefenses and students
        /// </summary>
        public PredefenseDateDTO PredefenseDate { get; set; }

        /// <summary>
        /// Teachers on predefense date
        /// </summary>
        public List<TeacherDTO> Teachers { get; set; }
    }
}
