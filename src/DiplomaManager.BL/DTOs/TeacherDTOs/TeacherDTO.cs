using System;
using DiplomaManager.BLL.DTOs.UserDTOs;

namespace DiplomaManager.BLL.DTOs.TeacherDTOs
{
    public class TeacherDTO : UserDTO
    {
        public int? PositionId { get; set; }

        public TeacherDTO() { }

        public TeacherDTO(string firstName, string lastName, string patronymic, int localeId, string email,
            DateTime creadionDate) 
            : base(firstName, lastName, patronymic, localeId, email, creadionDate)
        { }
    }
}
