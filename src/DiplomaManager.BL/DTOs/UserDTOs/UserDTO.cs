using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<PeopleNameDTO> PeopleNames
        { get; set; }

        public UserDTO() { }

        public UserDTO(string firstName, string lastName, string patronymic, int localeId, string email, DateTime creationDate)
        {
            PeopleNames = new List<PeopleNameDTO>
            {
                new PeopleNameDTO { Name = firstName, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKind.FirstName },
                new PeopleNameDTO { Name = lastName, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKind.LastName },
                new PeopleNameDTO { Name = patronymic, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKind.Patronymic },
            };
            Email = email;
        }

        public string GetFirstName(int localeId)
        {
            var nameDTO = PeopleNames.FirstOrDefault(f => f.LocaleId == localeId && f.NameKind == NameKind.FirstName);
            return nameDTO?.Name;
        }

        public string GetLastName(int localeId)
        {
            var nameDTO = PeopleNames.FirstOrDefault(f => f.LocaleId == localeId && f.NameKind == NameKind.LastName);
            return nameDTO?.Name;
        }

        public string GetPatronymic(int localeId)
        {
            var nameDTO = PeopleNames.FirstOrDefault(f => f.LocaleId == localeId && f.NameKind == NameKind.Patronymic);
            return nameDTO?.Name;
        }
    }
}
