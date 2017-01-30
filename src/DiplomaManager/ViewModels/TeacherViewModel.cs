namespace DiplomaManager.ViewModels
{
    public class TeacherViewModel : UserViewModel
    {
        public int PositionName { get; set; }

        public override string ToString()
        {
            return
                $"{FirstName} {LastName.Substring(1, 1)}. {Patronymic.Substring(1, 1)}.";
        }
    }
}
