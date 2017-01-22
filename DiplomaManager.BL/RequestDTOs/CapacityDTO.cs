using System;

namespace DiplomaManager.BL.RequestDTOs
{
    public class CapacityDTO
    {
        public int Id
        { get; set; }

        public int TeacherId { get; set; }

        public int DegreeId { get; set; }

        public int Count
        { get; set; }

        public int AcceptedCount
        { get; set; }

        public DateTime StudyingYear
        { get; set; }
    }
}
