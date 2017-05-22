using DiplomaManager.BLL.DTOs.TeacherDTOs;
using System;

namespace DiplomaManager.BLL.DTOs.RequestDTOs
{
    public class CapacityDTO
    {
        public int Id
        { get; set; }

        public int TeacherId { get; set; }

        public TeacherDTO Teacher { get; set; }

        public int DegreeId { get; set; }

        public int Count
        { get; set; }

        public int AcceptedCount
        { get; set; }

        public DateTime StudyingYear
        { get; set; }
    }
}
