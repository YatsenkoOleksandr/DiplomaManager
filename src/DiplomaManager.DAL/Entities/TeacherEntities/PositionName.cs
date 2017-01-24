using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.TeacherEntities
{
    public class PositionName
    {
        public int Id
        { get; set; }

        public int LocaleId
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
