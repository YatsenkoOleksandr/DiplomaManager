using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.ViewModels
{
    public class ProjectTitleViewModel
    {
        public int Id
        { get; set; }

        public int LocaleId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title
        { get; set; }
    }
}
