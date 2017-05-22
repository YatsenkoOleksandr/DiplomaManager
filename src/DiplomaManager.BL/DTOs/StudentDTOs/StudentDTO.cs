using System;
using DiplomaManager.BLL.DTOs.UserDTOs;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using System.Collections.Generic;

namespace DiplomaManager.BLL.DTOs.StudentDTOs
{
    public class StudentDTO : UserDTO
    {
        public int GroupId { get; set; }
        public GroupDTO Group { get; set; }

        public List<ProjectDTO> Projects { get; set; }

        public StudentDTO() { }

        public StudentDTO(string firstName, string lastName, string patronymic, int localeId, string email,
            DateTime creadionDate) 
            : base(firstName, lastName, patronymic, localeId, email, creadionDate)
        { }

        public StudentDTO(int firstNameId, int lastNameId, int patronymicId, int localeId, string email,
            DateTime creadionDate, int groupId)
            : base(firstNameId, lastNameId, patronymicId, localeId, email, creadionDate)
        {
            GroupId = groupId;
        }
    }
}
