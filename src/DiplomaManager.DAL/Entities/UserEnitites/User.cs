using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.UserEnitites
{
    public class User
    {
        public int Id
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email
        { get; set; }

        [MaxLength(20)]
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
