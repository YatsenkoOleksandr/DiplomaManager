using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.StudentEntities
{
    public class DegreeName
    {
        public int Id
        { get; set; }

        public int LocaleId
        { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name
        { get; set; }

        public int DegreeId { get; set; }
        public Degree Degree
        { get; set; }
    }
}
