using System;

namespace DiplomaManager.BL.EventsDTOs
{
    public class UndergraduateDefenseDTO
    {
        public int Id
        { get; set; }

        public int CommitteeId { get; set; }

        public int StudentId { get; set; }

        public DateTime Date
        { get; set; }
        
        public bool Passed
        { get; set; }  
        
        public int SoftwareReadiness
        { get; set; }

        public int WritingReadiness
        { get; set; }
        
        public int PresentationReadiness
        { get; set; }
        
        public bool ReportExist
        { get; set; }
        
        public bool SafetySigned
        { get; set; }
        
        public bool EconomySigned
        { get; set; }
        
        public bool ControlSigned
        { get; set; }                  
    }
}
