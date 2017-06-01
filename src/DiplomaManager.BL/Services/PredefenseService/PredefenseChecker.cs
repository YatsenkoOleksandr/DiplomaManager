using DiplomaManager.BLL.Exceptions;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaManager.BLL.Services.PredefenseService
{
    /// <summary>
    /// Class for checking existance or ability to do some action
    /// </summary>
    class PredefenseChecker
    {
        private readonly IDiplomaManagerUnitOfWork _database;        

        public PredefenseChecker(IDiplomaManagerUnitOfWork uow)
        {
            _database = uow;            
        }

        public void CheckStudentExistance(int studentId)
        {
            if (_database.Students.Get(studentId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного студента.");
            }
        }

        public void CheckTeacherExistance(int teacherId)
        {
            if (_database.Teachers.Get(teacherId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного преподавателя.");
            }
        }

        public void CheckPredefensePeriodExistance(int predefensePeriodId)
        {
            if (_database.PredefensePeriods.Get(predefensePeriodId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного периода предзащит.");
            }
        }

        public void CheckPredefenseDateExistance(int predefenseDateId)
        {
            if (_database.PredefenseDates.Get(predefenseDateId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанный день проведения предзащиты.");
            }
        }

        public void CheckPredefenseExistance(int predefenseId)
        {
            if (_database.Predefenses.Get(predefenseId) == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанного времени проведения предзащиты.");
            }
        }

        public void CheckTeacherAccessToPredefensePeriod(int teacherId, int predefensePeriodId)
        {
            IEnumerable<PredefenseTeacherCapacity> capacities =
                _database.PredefenseTeacherCapacities.Get(
                    new FilterExpression<PredefenseTeacherCapacity>(ptc =>
                    ptc.TeacherId == teacherId && ptc.PredefensePeriodId == predefensePeriodId));
            if (capacities.Count() == 0)
            {
                throw new IncorrectActionException("Преподаватель не имеет доступа к периоду проведения предзащит.");
            }
        }


        public void CheckTeacherAccessToPredefense(int teacherId, int predefenseId)
        {
            Predefense pred = _database.Predefenses.Get(predefenseId);

            CheckPredefenseDateExistance(pred.PredefenseDateId);
            PredefenseDate predDate = _database.PredefenseDates.Get(pred.PredefenseDateId);

            Appointment appointment = _database.Appointments.Get(new FilterExpression<Appointment>(ap =>
                    ap.PredefenseDateId == predDate.Id && ap.TeacherId == teacherId))
                .FirstOrDefault();
            if (appointment == null)
            {
                throw new IncorrectActionException("Преподаватель не имеет доступа к редактированию результатов предзащиты.");
            }
        }
    }
}
