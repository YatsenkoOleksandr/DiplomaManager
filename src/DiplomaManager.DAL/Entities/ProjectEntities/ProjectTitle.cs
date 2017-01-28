using System;
using System.ComponentModel.DataAnnotations;
using DiplomaManager.DAL.Entities.SharedEntities;

namespace DiplomaManager.DAL.Entities.ProjectEntities
{
    public class ProjectTitle
    {
        public int Id
        { get; set; }

        public int ProjectId { get; set; }
        public Project Project
        { get; set; }

        public int LocaleId { get; set; }
        public Locale Locale
        { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
