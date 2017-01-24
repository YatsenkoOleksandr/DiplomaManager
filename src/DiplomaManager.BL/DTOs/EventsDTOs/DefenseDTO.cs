using System;

namespace DiplomaManager.BLL.DTOs.EventsDTOs
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
