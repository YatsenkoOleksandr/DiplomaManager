using DiplomaManager.BLL.Configuration;
using DiplomaManager.BLL.Exceptions;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.PredefenseEntities;
using DiplomaManager.DAL.Entities.StudentEntities;
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
    /// Class desribes logic of submition and denying submition to predefense
    /// </summary>
    public class PredefenseSubmitter
    {
        private readonly IDiplomaManagerUnitOfWork _database;
        private readonly ILocaleConfiguration _cultureConfiguration;
        private readonly IEmailService _emailService;

        public PredefenseSubmitter(IDiplomaManagerUnitOfWork uow, ILocaleConfiguration configuration, IEmailService emailService)
        {
            _database = uow;
            _cultureConfiguration = configuration;
            _emailService = emailService;
        }

        public void SubmitStudentToPredefense(int studentId, int predefenseId)
        {
            Student student = _database.Students.Get(
                new FilterExpression<Student>[]
                {
                    new FilterExpression<Student>(s => s.Id == studentId)
                },
                new IncludeExpression<Student>[]
                {
                    new IncludeExpression<Student>(s => s.Group),
                    new IncludeExpression<Student>(s => s.Predefenses)
                }).FirstOrDefault();
            // Check, if exists student
            if (student == null)
            {
                throw new NoEntityInDatabaseException("Указанного студента не найдено.");
            }
            // Check, if student doesn't have passed predefenses
            if (student.Predefenses.Any(p => p.Passed == true))
            {
                throw new IncorrectActionException("Студент уже прошёл предзащиту.");
            }
            // Check, if student doesn't have active predefenses
            if (student.Predefenses.Any(p => p.Passed == null || p.Passed == true))
            {
                throw new IncorrectActionException("Студент уже записан на предзащиту.");
            }

            Predefense predefense = _database.Predefenses.Get(
                new FilterExpression<Predefense>[]
                {
                    new FilterExpression<Predefense>(p => p.Id == predefenseId)
                },
                new IncludeExpression<Predefense>[]
                {
                    new IncludeExpression<Predefense>(p => p.PredefenseDate.PredefensePeriod)
                }).FirstOrDefault();

            // Check, if exists predefense
            if (predefense == null)
            {
                throw new NoEntityInDatabaseException("Указанной предзащиты не найдено.");
            }

            // Check, if degrees matches
            if (predefense.PredefenseDate.PredefensePeriod.DegreeId != student.Group.DegreeId)
            {
                throw new IncorrectActionException("Образовательные уровни студента и предзащиты не совпадают.");
            }
            // Check, if graduation years matches
            if (predefense.PredefenseDate.PredefensePeriod.GraduationYear != student.Group.GraduationYear)
            {
                throw new IncorrectActionException("Год выпуска студента и предзащиты не совпадают.");
            }

            predefense.StudentId = studentId;
            _database.Predefenses.Update(predefense);
            _database.Save();
        }

        public void DenyStudentSubmitToPredefense(int studentId, int predefenseId)
        {
            Student student = _database.Students.Get(
                new FilterExpression<Student>[]
                {
                    new FilterExpression<Student>(s => s.Id == studentId)
                },
                new IncludeExpression<Student>[]
                {
                    new IncludeExpression<Student>(s => s.Group),
                    new IncludeExpression<Student>(s => s.Predefenses)
                }).FirstOrDefault();
            // Check, if exists student
            if (student == null)
            {
                throw new NoEntityInDatabaseException("Указанного студента не найдено.");
            }
            // Find removed predefense
            Predefense studentPredefense = student.Predefenses.Where(p => p.Id == predefenseId).FirstOrDefault();
            if (studentPredefense == null)
            {
                throw new NoEntityInDatabaseException("У студента не найдено указанной предзащиты.");
            }
            // Deny student submit
            studentPredefense.StudentId = null;
            _database.Predefenses.Update(studentPredefense);
            _database.Save();
        }

        public void SubmitTeacherToPredefenseDate(int teacherId, int predefenseDateId)
        {
            PredefenseDate predefenseDate = _database.PredefenseDates.Get(predefenseDateId);
            if (predefenseDate == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанный день проведения предзащиты.");
            }

            PredefenseTeacherCapacity capacity = _database.PredefenseTeacherCapacities.Get(
                new FilterExpression<PredefenseTeacherCapacity>(ptc =>
                    ptc.PredefensePeriodId == predefenseDate.PredefensePeriodId &&
                    ptc.TeacherId == teacherId)).FirstOrDefault();
            if (capacity == null)
            {
                throw new IncorrectActionException("У преподавателя нет назначения посещать предзащиты в данном периоде.");
            }

            if (capacity.Total == capacity.Count)
            {
                throw new IncorrectActionException("У преподавателя исчерпан лимит посещения предзащит.");
            }

            // Check if teacher is free at predefense date
            IEnumerable<Appointment> appointments = _database.Appointments.Get(
                new FilterExpression<Appointment>(ap => ap.TeacherId == teacherId &&
                    ap.PredefenseDate.Date.Date == predefenseDate.Date.Date),
                new IncludeExpression<Appointment>[]
                {
                    new IncludeExpression<Appointment>(app => app.PredefenseDate)
                });

            foreach (var app in appointments)
            {
                // Teacher has predefense at same time
                if (!(app.PredefenseDate.FinishTime < predefenseDate.BeginTime ||
                    app.PredefenseDate.BeginTime > predefenseDate.FinishTime))
                {
                    throw new IncorrectActionException("У преподавателя есть предзащита в то же самое время.");
                }
            }

            // Teacher can visit predefense
            _database.Appointments.Add(new Appointment()
            {
                PredefenseDateId = predefenseDateId,
                TeacherId = teacherId
            });
            capacity.Count++;
            _database.PredefenseTeacherCapacities.Update(capacity);
            _database.Save();
        }

        public void DenyTeacherSubmitToPredefenseDate(int teacherId, int predefenseDateId)
        {
            PredefenseDate predefenseDate = _database.PredefenseDates.Get(predefenseDateId);
            if (predefenseDate == null)
            {
                throw new NoEntityInDatabaseException("Не найдено указанный день проведения предзащиты.");
            }

            Appointment appointment = _database.Appointments.Get(new FilterExpression<Appointment>(
                ap => ap.TeacherId == teacherId && ap.PredefenseDateId == predefenseDateId)).FirstOrDefault();
            if (appointment == null)
            {
                throw new IncorrectActionException("Преподаватель не был записан на указанный день предзащит.");
            }

            PredefenseTeacherCapacity capacity = _database.PredefenseTeacherCapacities.Get(
                new FilterExpression<PredefenseTeacherCapacity>(
                    ptc => ptc.PredefensePeriodId == predefenseDate.PredefensePeriodId && ptc.TeacherId == teacherId))
                .FirstOrDefault();
            if (capacity == null)
            {
                throw new NoEntityInDatabaseException("Не найдено назначения преподавателя на присутствие в данном периоде предзащит.");
            }

            capacity.Count--;
            _database.PredefenseTeacherCapacities.Update(capacity);
            _database.Appointments.Remove(appointment);
            _database.Save();
        }
    }
}
