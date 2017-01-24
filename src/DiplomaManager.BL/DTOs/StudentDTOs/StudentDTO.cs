using DiplomaManager.BLL.DTOs.UserDTOs;

namespace DiplomaManager.BLL.DTOs.StudentDTOs
{
    public class StudentDTO : UserDTO
    {
        public int GroupId { get; set; }

        public int DefenseId { get; set; }
    }
}
