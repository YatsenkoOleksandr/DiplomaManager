using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.Areas.Admin.ViewModels
{
    public class DevelopmentAreaViewModel
    {
        public int Id
        { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "��������")]
        public string Name
        { get; set; }
    }
}
