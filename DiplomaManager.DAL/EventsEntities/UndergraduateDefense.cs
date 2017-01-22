using System;
using DiplomaManager.DAL.StudentEntities;

namespace DiplomaManager.DAL.EventsEntities
{
    public class UndergraduateDefense
    {
        public int Id
        { get; set; }

        public int CommitteeId { get; set; }
        public Committee Committee
        { get; set; }

        public int StudentId { get; set; }
        public Student Student
        { get; set; }

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
