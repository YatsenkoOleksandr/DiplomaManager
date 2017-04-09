using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.UserEnitites
{
    public class User
    {
        public int Id
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

        public List<PeopleName> PeopleNames
        { get; set; }
    }
}