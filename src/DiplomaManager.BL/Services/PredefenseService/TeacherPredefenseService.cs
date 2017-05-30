using DiplomaManager.BLL.Interfaces.PredefenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.Extensions.PredefenseService;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Interfaces;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    public class TeacherPredefenseService : ITeacherPredefenseService
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;

        public TeacherPredefenseService(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }

        public IEnumerable<TeacherPredefensePeriod> GetTeacherPredefensePeriods(int teacherId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PredefenseDateDTO> GetTeacherPredefenseDates(int teacherId, int predefensePeriodId)
        {
            throw new NotImplementedException();
        }

        public PredefenseDTO GetPredefenseResults(int teacherId, int predefenseId)
        {
            throw new NotImplementedException();
        }

        public void SavePredefenseResults(int teacherId, PredefenseDTO predefense)
        {
            throw new NotImplementedException();
        }

        public void SubmitToPredefenseDate(int teacherId, int predefenseDateId)
        {
            throw new NotImplementedException();
        }

        public void DenySubmitToPredefenseDate(int teacherId, int predefenseDateId)
        {
            throw new NotImplementedException();
        }
    }
}
