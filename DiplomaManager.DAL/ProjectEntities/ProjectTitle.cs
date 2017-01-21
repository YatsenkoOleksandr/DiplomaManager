using System;

namespace DiplomaManager.DAL.ProjectEntities
{
    public class ProjectTitle
    {
        public int ID
        { get; set; }

        public Project Project
        { get; set; }

        public int LocaleID
        { get; set; }

        public string Title
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
