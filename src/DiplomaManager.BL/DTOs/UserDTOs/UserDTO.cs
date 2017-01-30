using System;
using System.Collections.Generic;
using DiplomaManager.DAL.Entities.UserEnitites;

namespace DiplomaManager.BLL.DTOs.UserDTOs
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

        public List<FirstNameDTO> FirstNames
        { get; set; }

        public List<LastNameDTO> LastNames
        { get; set; }

        public List<PatronymicDTO> Patronymics
        { get; set; }
    }
}
