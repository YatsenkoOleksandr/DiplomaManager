using System;
using System.Collections.Generic;

namespace DiplomaManager.DAL.ProjectEntities
{
    public class Project
    {
        public int ID
        { get; set; }

        public StudentEntities.Student Student
        { get; set; }

        public TeacherEntities.Teacher Teacher
        { get; set; }

        public RequestEntities.DevelopmentArea DevelopmentArea
        { get; set; }

        public DateTime CreationDate
        { get; set; }

        public DateTime PracticeJournalPassed
        { get; set; }

        public bool Accepted
        { get; set; }

        public List<ProjectTitle> ProjectsTitles
        { get; set; }
    }
}
