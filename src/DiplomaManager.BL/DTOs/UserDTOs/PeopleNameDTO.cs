using System;

namespace DiplomaManager.BLL.DTOs.UserDTOs
{
    public enum NameKind
    {
        FirstName,
        LastName,
        Patronymic
    }

    public class PeopleNameDTO
    {
        public int Id
        { get; set; }

        public int LocaleId
        { get; set; }

        public int UserId { get; set; }

        public string Name
        { get; set; }

        public NameKind NameKind { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
