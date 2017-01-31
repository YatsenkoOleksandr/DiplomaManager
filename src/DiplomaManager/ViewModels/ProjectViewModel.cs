using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.ViewModels
{
    public class ProjectViewModel
    {
        [Display(Name = "Преподаватель")]
        public int TeacherId { get; set; }

        [Display(Name = "Образовательный уровень")]
        public int DegreeId { get; set; }

        [Display(Name = "Предметная область")]
        public int DevelopmentAreaId { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Тема работы")]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
    }
}
