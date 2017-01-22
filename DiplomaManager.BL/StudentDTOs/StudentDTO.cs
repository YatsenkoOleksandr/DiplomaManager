using DiplomaManager.BL.UserDTOs;

namespace DiplomaManager.BL.StudentDTOs
{
    public class StudentDTO : UserDTO
    {
        public int GroupId { get; set; }

        public int DefenseId { get; set; }
    }
}
