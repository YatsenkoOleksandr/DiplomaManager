using System;
using System.Collections.Generic;

namespace DiplomaManager.DAL.UserEnitites
{
    public class User
    {
        public int ID
        { get; set; }

        public string Login
        { get; set; }

        public string Password
        { get; set; }

        public string Email
        { get; set; }

        public string Status
        { get; set; }

        public DateTime StatusCreationDate
        { get; set; }

        public List<FirstName> FirstNames
        { get; set; }

        public List<LastName> LastNames
        { get; set; }

        public List<Patronymic> Patronymics
        { get; set; }
    }
}
