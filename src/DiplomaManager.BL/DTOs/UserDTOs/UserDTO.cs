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

        public UserDTO(int id, string email)
        {
            Id = id;
            Email = email;
        }

        public UserDTO(string firstName, string lastName, string patronymic, int localeId, string email, DateTime creationDate)
        {
            PeopleNames = new List<PeopleNameDTO>
            {
                new PeopleNameDTO { Name = firstName, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKindDTO.FirstName },
                new PeopleNameDTO { Name = lastName, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKindDTO.LastName },
                new PeopleNameDTO { Name = patronymic, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKindDTO.Patronymic },
            };
            Email = email;
        }

        public UserDTO(int firstNameId, int lastNameId, int patronymicId, int localeId, string email, DateTime creationDate)
        {
            PeopleNames = new List<PeopleNameDTO>
            {
                new PeopleNameDTO { Id = firstNameId, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKindDTO.FirstName },
                new PeopleNameDTO { Id = lastNameId, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKindDTO.LastName },
                new PeopleNameDTO { Id = patronymicId, LocaleId = localeId, CreationDate = creationDate, NameKind = NameKindDTO.Patronymic },
            };
            Email = email;
        }

        public string GetFirstName(int localeId)
        {
            var nameDTO = PeopleNames.FirstOrDefault(f => f.LocaleId == localeId && f.NameKind == NameKindDTO.FirstName);
            return nameDTO?.Name;
        }

        public string GetLastName(int localeId)
        {
            var nameDTO = PeopleNames.FirstOrDefault(f => f.LocaleId == localeId && f.NameKind == NameKindDTO.LastName);
            return nameDTO?.Name;
        }

        public string GetPatronymic(int localeId)
        {
            var nameDTO = PeopleNames.FirstOrDefault(f => f.LocaleId == localeId && f.NameKind == NameKindDTO.Patronymic);
            return nameDTO?.Name;
        }

        public string GetFullName(int localeId = 3)
        {
            string ln = this.GetLastName(localeId);
            string fn = this.GetFirstName(localeId);
            string pn = this.GetPatronymic(localeId);

            return string.Concat(ln ?? "-", " ", fn ?? "-", " ", pn ?? "-");
        }

        public string GetShortName(int localeId = 3)
        {
            string ln = this.GetLastName(localeId);
            string fn = this.GetFirstName(localeId);
            string pn = this.GetPatronymic(localeId);

            return string.Concat(ln ?? "-", " ", fn?.Substring(0, 1) ?? "-", ".", pn?.Substring(0, 1) ?? "-", ".");
        }

        public override bool Equals(object obj)
        {
            var user = obj as UserDTO;
            if (user == null) return false;
            return (Id == user.Id);
        }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + Id.GetHashCode();
            return hash;
        }
    }
}