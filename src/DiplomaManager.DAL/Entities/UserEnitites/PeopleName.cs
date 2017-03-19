using System;
using System.ComponentModel.DataAnnotations;
using DiplomaManager.DAL.Entities.SharedEntities;

namespace DiplomaManager.DAL.Entities.UserEnitites
{
    public enum NameKind
    {
        FirstName,
        LastName,
        Patronymic
    }

    public class PeopleName
    {
        public int Id
        { get; set; }

        public int LocaleId { get; set; }
        public Locale Locale
        { get; set; }

        public int UserId { get; set; }
        public User User
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name
        { get; set; }

        public NameKind NameKind { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
