using DiplomaManager.ViewModels;

namespace DiplomaManager.Areas.Teacher.ViewModels
{
    public class StudentViewModel : UserViewModel
    {
        public int GroupId { get; set; }
    }

    public class StudentInfoViewModel : UserInfoViewModel
    {
        public string GroupName { get; set; }
    }
}
