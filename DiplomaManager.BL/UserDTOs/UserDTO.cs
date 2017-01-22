using System;
using System.Collections.Generic;
using DiplomaManager.DAL.UserEnitites;

namespace DiplomaManager.BL.UserDTOs
{
    public class UserDTO
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

        public List<FirstName> FirstNames
        { get; set; }

        public List<LastName> LastNames
        { get; set; }

        public List<PatronymicDTO> Patronymics
        { get; set; }
    }
}
