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

        public List<FirstNameDTO> FirstNames
        { get; set; }

        public List<LastNameDTO> LastNames
        { get; set; }

        public List<PatronymicDTO> Patronymics
        { get; set; }

        public UserDTO() { }

        public UserDTO(string firstName, string lastName, string patronymic, int localeId, string email, DateTime creationDate)
        {
            FirstNames = new List<FirstNameDTO> { new FirstNameDTO { Name = firstName, LocaleId = localeId, CreationDate = creationDate } };
            LastNames = new List<LastNameDTO> { new LastNameDTO { Name = lastName, LocaleId = localeId, CreationDate = creationDate } };
            Patronymics = new List<PatronymicDTO> { new PatronymicDTO { Name = patronymic, LocaleId = localeId, CreationDate = creationDate } };
            Email = email;
        }

        public string GetFirstName(int localeId)
        {
            var nameDTO = FirstNames.FirstOrDefault(f => f.LocaleId == localeId);
            return nameDTO?.Name;
        }

        public string GetLastName(int localeId)
        {
            var nameDTO = LastNames.FirstOrDefault(f => f.LocaleId == localeId);
            return nameDTO?.Name;
        }

        public string GetPatronymic(int localeId)
        {
            var nameDTO = Patronymics.FirstOrDefault(f => f.LocaleId == localeId);
            return nameDTO?.Name;
        }
    }
}
