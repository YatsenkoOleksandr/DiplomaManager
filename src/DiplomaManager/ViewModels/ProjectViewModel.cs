using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.ViewModels
{
    public class ProjectViewModel
    {
        [Display(Name = "Преподаватель")]
        public int TeacherId { get; set; }

        [Display(Name = "Предметная область")]
        public int DevelopmentAreaId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Тема работы")]
        public string Title { get; set; }
    }
}
