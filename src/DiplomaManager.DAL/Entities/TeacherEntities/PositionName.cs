using System.ComponentModel.DataAnnotations;
using DiplomaManager.DAL.Entities.SharedEntities;

namespace DiplomaManager.DAL.Entities.TeacherEntities
{
    public class PositionName
    {
        public int Id
        { get; set; }

        public int LocaleId { get; set; }
        public Locale Locale
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name
        { get; set; }

        public int PositionId { get; set; }
        public Position Position
        { get; set; }
    }
}
