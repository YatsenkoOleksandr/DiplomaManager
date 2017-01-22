using System;

namespace DiplomaManager.DAL.UserEnitites
{
    public class FirstName
    {
        public int Id
        { get; set; }

        public int LocaleId
        { get; set; }

        public int UserId { get; set; }
        public User User
        { get; set; }

        public string Name
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
