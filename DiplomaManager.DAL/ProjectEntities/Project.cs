using System;
using System.Collections.Generic;
using DiplomaManager.DAL.RequestEntities;
using DiplomaManager.DAL.StudentEntities;
using DiplomaManager.DAL.TeacherEntities;

namespace DiplomaManager.DAL.ProjectEntities
{
    public class Project
    {
        public int Id
        { get; set; }

        public int StudentId { get; set; }
        public Student Student
        { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher
        { get; set; }

        public int DevelopmentAreaId { get; set; }
        public DevelopmentArea DevelopmentArea
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
