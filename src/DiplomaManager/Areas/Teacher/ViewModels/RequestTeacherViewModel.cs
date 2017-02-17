using System;
using DiplomaManager.ViewModels;

namespace DiplomaManager.Areas.Teacher.ViewModels
{
    public class RequestTeacherViewModel
    {
        public int Id;

        public string ProjectTitle { get; set; }

        public UserViewModel Student { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Accepted { get; set; }
    }
}
