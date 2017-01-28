using System.ComponentModel.DataAnnotations;
using DiplomaManager.DAL.Entities.SharedEntities;

namespace DiplomaManager.DAL.Entities.StudentEntities
{
    public class DegreeName
    {
        public int Id
        { get; set; }

        public int LocaleId { get; set; }
        public Locale Locale
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
