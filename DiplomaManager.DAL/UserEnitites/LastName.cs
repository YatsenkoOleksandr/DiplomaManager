using System;

namespace DiplomaManager.DAL.UserEnitites
{
    public class LastName
    {
        public int ID
        { get; set; }

        public int LocaleID
        { get; set; }

        public User User
        { get; set; }

        public string Name
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
