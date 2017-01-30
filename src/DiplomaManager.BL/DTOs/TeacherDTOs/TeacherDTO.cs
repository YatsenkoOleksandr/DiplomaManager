using DiplomaManager.BLL.DTOs.UserDTOs;

namespace DiplomaManager.BLL.DTOs.TeacherDTOs
{
    public class TeacherDTO : UserDTO
    {
        public int? PositionId { get; set; }
    }
}
