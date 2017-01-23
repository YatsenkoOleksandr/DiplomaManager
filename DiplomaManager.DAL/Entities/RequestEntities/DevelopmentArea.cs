using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.RequestEntities
{
    public class DevelopmentArea
    {
        public int Id
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name
        { get; set; }

        public List<Interest> Interests
        { get; set; }
    }
}
