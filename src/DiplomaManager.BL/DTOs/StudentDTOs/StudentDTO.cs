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

        public StudentDTO(int id, string email, int groupId)
        {
            Id = id;
            Email = email;
            GroupId = groupId;
        }
    }
}
