using System;

namespace DiplomaManager.BL.UserDTOs
{
    public class FirstNameDTO
    {
        public int Id
        { get; set; }

        public int LocaleId
        { get; set; }

        public int UserId { get; set; }

        public string Name
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
