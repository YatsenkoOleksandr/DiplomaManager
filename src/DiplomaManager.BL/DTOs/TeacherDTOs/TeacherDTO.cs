using System;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.ProjectDTOs;

namespace DiplomaManager.BLL.DTOs.TeacherDTOs
{
    public class TeacherDTO : UserDTO
    {
        public int? PositionId { get; set; }        

        public TeacherDTO() { }

        public TeacherDTO(string firstName, string lastName, string patronymic, int localeId, string email,
            DateTime creationDate) 
            : base(firstName, lastName, patronymic, localeId, email, creationDate)
        { }
    }
}
