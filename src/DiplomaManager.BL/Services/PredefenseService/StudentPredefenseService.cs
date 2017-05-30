﻿using DiplomaManager.BLL.Interfaces.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.DTOs.StudentDTOs;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Interfaces;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    public class StudentPredefenseService : IStudentPredefenseService
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;

        public StudentPredefenseService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }

        public IEnumerable<DegreeDTO> GetDegrees()
        {             
            throw new NotImplementedException();
        }

        public IEnumerable<StudentDTO> GetFreeStudents(int groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetGraduationYears(int degreeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupDTO> GetGroups(int degreeId, int graduationYear)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PredefenseDateDTO> GetPredefenseSchedule(int degreeId, int graduationYear)
        {
            throw new NotImplementedException();
        }

        public void SubmitPredefense(int studentId, int predefenseId)
        {
            throw new NotImplementedException();
        }
    }
}
