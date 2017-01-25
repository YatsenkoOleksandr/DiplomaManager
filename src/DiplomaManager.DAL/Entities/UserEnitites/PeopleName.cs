using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.UserEnitites
{
    public class PeopleName
    {
        public int Id
        { get; set; }

        public int LocaleId
        { get; set; }

        public int UserId { get; set; }
        public User User
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
