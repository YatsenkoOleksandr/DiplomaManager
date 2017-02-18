using System;
using System.Collections.Generic;
using DiplomaManager.BLL.DTOs.ProjectDTOs;
using DiplomaManager.ViewModels;

namespace DiplomaManager.Areas.Teacher.ViewModels
{
    public class RequestTeacherViewModel
    {
        public int Id;

        public List<ProjectTitleDTO> ProjectTitles { get; set; }

        public UserViewModel Student { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Accepted { get; set; }
    }
}
