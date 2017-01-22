using System;

namespace DiplomaManager.DAL.ProjectEntities
{
    public class ProjectTitle
    {
        public int Id
        { get; set; }

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
