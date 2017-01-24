using System;

namespace DiplomaManager.BLL.DTOs.ProjectDTOs
{
    public class ProjectTitleDTO
    {
        public int Id
        { get; set; }

        public int ProjectId { get; set; }

        public int LocaleId
        { get; set; }

        public string Title
        { get; set; }

        public DateTime CreationDate
        { get; set; }
    }
}
