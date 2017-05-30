using System;
using DiplomaManager.DAL.Entities.StudentEntities;

namespace DiplomaManager.DAL.Entities.PredefenseEntities
{
    public class Predefense
    {
        public int Id { get; set; }
        
        public int PredefenseDateId { get; set; }
        public PredefenseDate PredefenseDate { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }        
        
        public bool? Passed { get; set; }  
        
        public int SoftwareReadiness { get; set; }

        public int WritingReadiness { get; set; }
        
        public int PresentationReadiness { get; set; }
        
        public bool ReportExist { get; set; }
        
        public bool SafetySigned { get; set; }
        
        public bool EconomySigned { get; set; }
        
        public bool ControlSigned { get; set; }                  
    }
}
