using System;
using DiplomaManager.BLL.DTOs.UserDTOs;

namespace DiplomaManager.BLL.DTOs.StudentDTOs
{
    public class StudentDTO : UserDTO
    {
        public int GroupId { get; set; }
        public GroupDTO Group { get; set; }

        public StudentDTO() { }

        public StudentDTO(string firstName, string lastName, string patronymic, int localeId, string email,
            DateTime creadionDate) 
            : base(firstName, lastName, patronymic, localeId, email, creadionDate)
        { }
    }
}
