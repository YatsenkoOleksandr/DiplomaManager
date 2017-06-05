using DiplomaManager.BLL.DTOs.TeacherDTOs;

namespace DiplomaManager.BLL.DTOs.PredefenseDTOs
{
    public class PredefenseTeacherCapacityDTO
    {
        public int Id { get; set; }

        public int PredefensePeriodId { get; set; }       

        public int TeacherId { get; set; }  
        public TeacherDTO Teacher { get; set; }

        public int Total { get; set; }

        public int Count { get; set; }
    }
}
