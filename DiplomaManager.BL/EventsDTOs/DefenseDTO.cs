using System;

namespace DiplomaManager.BL.EventsDTOs
{
    public class DefenseDTO
    {
        public int Id
        { get; set; }

        public int StudentId { get; set; }

        public DateTime Date
        { get; set; }

        public int Estimate
        { get; set; }
    }
}
