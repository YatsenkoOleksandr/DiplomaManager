using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.ViewModels
{
    public class DevelopmentAreaViewModel
    {
        public int Id
        { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Название")]
        public string Name
        { get; set; }
    }
}
