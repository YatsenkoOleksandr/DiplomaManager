namespace DiplomaManager.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public int FirstNameId { get; set; }

        public int LastNameId { get; set; }

        public int PatronymicId { get; set; }

        public string Email { get; set; }
    }

    public class UserInfoViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return
                $"{LastName} {FirstName.Substring(0, 1)}. {Patronymic.Substring(0, 1)}.";
        }
    }
}
