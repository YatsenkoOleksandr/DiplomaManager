using System;

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

        public string Title
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
