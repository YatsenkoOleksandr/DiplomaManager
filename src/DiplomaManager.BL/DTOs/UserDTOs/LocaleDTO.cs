using System.Globalization;

namespace DiplomaManager.BLL.DTOs.UserDTOs
{
    public class LocaleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NativeName { get; set; }

        public CultureInfo CultureInfo => new CultureInfo(Name);
    }
}
