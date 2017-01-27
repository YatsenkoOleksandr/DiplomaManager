using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaManager.DAL.Entities.ProjectEntities
{
    public class ProjectTitle
    {
        public int Id
        { get; set; }

        public int ProjectId { get; set; }
        public Project Project
        { get; set; }

        public int LocaleId
        { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
