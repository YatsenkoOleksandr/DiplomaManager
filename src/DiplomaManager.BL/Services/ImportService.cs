using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.DAL.Entities.StudentEntities;
using DiplomaManager.DAL.Entities.UserEnitites;
using DiplomaManager.DAL.Interfaces;
using DiplomaManager.DAL.Utils;
using OfficeOpenXml;
using Group = DiplomaManager.DAL.Entities.StudentEntities.Group;

namespace DiplomaManager.BLL.Services
{
    public class ImportService : IImportService
    {
        private IDiplomaManagerUnitOfWork Database { get; }
        private IUserService UserService { get; }

        private RowProcessingResult _rowProcessingInfo;

        public ImportService(IDiplomaManagerUnitOfWork uow, IUserService userService)
        {
            Database = uow;
            UserService = userService;
        }

        public RowProcessingResult ImportStudentsInfo(Stream excelFileStream)
        {
            _rowProcessingInfo = new RowProcessingResult();

            var excelGroup = GetGroupExcel(excelFileStream, 1, 1);
            if (excelGroup == null) throw new InvalidOperationException("Can't get Group");
            var group = ProcessGroup(excelGroup);

            var excelFullNames = GetFullNamesExcel(excelFileStream, 3, 2);
            if (excelFullNames != null)
            {
                var excelFullNamesList = excelFullNames.ToList();
                ProcessFullNames(excelFullNamesList, group);
            }

            return _rowProcessingInfo;
        }

        private void ProcessFullNames(IEnumerable<string> fullNames, Group group)
        {
            foreach (var fullName in fullNames)
            {
                try
                {
                    var peopleNames = GetPeopleNames(fullName).ToList();
                    var peopleNamesProcessed = ProcessPeopleNames(peopleNames);
                    ProcessStudent(peopleNamesProcessed, group);

                    _rowProcessingInfo.ValidRowsCount++;
                }
                catch (Exception)
                {
                    _rowProcessingInfo.InvalidRowsCount++;
                }
                finally
                {
                    _rowProcessingInfo.ProcessedRowsCount++;
                }
            }
        }

        private void ProcessStudent(IEnumerable<PeopleName> peopleNames, Group group)
        {
            var peopleNamesList = peopleNames.ToList();
            var studentDb = UserService.GetUserFromFullName<Student>(peopleNamesList);
            if (studentDb != null) return; //If Studenb exists in Db
            var student = new Student
            {
                StatusCreationDate = DateTime.Now,
                Group = group,
                PeopleNames = peopleNamesList
            };
            Database.Students.Add(student);
            Database.Save();
        }

        private IEnumerable<PeopleName> ProcessPeopleNames(IEnumerable<PeopleName> peopleNames)
        {
            var peopleNamesList = new List<PeopleName>();
            foreach (var pName in peopleNames)
            {
                var pNameDb =
                    Database.PeopleNames.Get(new FilterExpression<PeopleName>(pn => pn.Name == pName.Name)).SingleOrDefault();

                if (pNameDb == null) //If PeopleName exists in Db
                {
                    pName.CreationDate = DateTime.Now;
                    Database.PeopleNames.Add(pName);
                    peopleNamesList.Add(pName);
                }
                else
                {
                    peopleNamesList.Add(pNameDb);
                }
            }
            Database.Save();
            return peopleNamesList;
        }

        private IEnumerable<PeopleName> GetPeopleNames(string fullName)
        {
            var fullNameSplit = fullName.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var lastName = new PeopleName
            {
                Name = fullNameSplit[0],
                LocaleId = 1, //Invariant Language
                NameKind = NameKind.LastName
            };
            var firstName = new PeopleName
            {
                Name = fullNameSplit[1],
                LocaleId = 1,
                NameKind = NameKind.FirstName
            };

            if (fullNameSplit.Length == 2) //If Patronymic is missing
            {
                return new[] { firstName, lastName };
            }

            var patronymic = new PeopleName
            {
                Name = fullNameSplit[2],
                LocaleId = 1,
                NameKind = NameKind.Patronymic
            };
            return new[] { firstName, lastName, patronymic };
        }

        private IEnumerable<string> GetFullNamesExcel(Stream excelFileStream, int rowStart, int col)
        {
            using (var package = new ExcelPackage(excelFileStream))
            {
                var worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;

                _rowProcessingInfo.RowsCount += rowCount;

                int colCount = worksheet.Dimension.Columns;
                var peopleNames = new List<string>();
                for (int row = rowStart; row <= rowCount; row++)
                {
                    if (IsEmptyRow(worksheet, row, colCount))
                        break;

                    var fullName = worksheet.Cells[row, col].Value;

                    if (fullName != null && FullNameIsValid(fullName.ToString()))
                    {
                        peopleNames.Add(fullName.ToString());
                    }
                    else
                    {
                        _rowProcessingInfo.ProcessedRowsCount++;
                        _rowProcessingInfo.InvalidRowsCount++;
                        _rowProcessingInfo.InvalidRowsNumbers.Add(row);
                    }
                }
                return peopleNames;
            }
        }

        private static bool IsEmptyRow(ExcelWorksheet worksheet, int row, int colCount)
        {
            var currentRow = worksheet.Cells[row, 1, row, colCount];
            return currentRow.All(c => string.IsNullOrEmpty(c.Value?.ToString()));
        }

        private static bool FullNameIsValid(string fullName)
        {
            const string fullNameRegexStr =
                @"[\p{IsCyrillic}\p{IsCyrillicSupplement}]+[\-\s]+([\p{IsCyrillic}\p{IsCyrillicSupplement}]+[\-\s]?){2,}$";
            var fullNameRegex = new Regex(fullNameRegexStr);
            return fullNameRegex.IsMatch(fullName);
        }

        private Group ProcessGroup(string groupName)
        {
            var groupsDb = Database.Groups.Get(new FilterExpression<Group>(g => g.Name == groupName)).ToList();
            if (groupsDb.Count > 0) //If Group exists in Db
                return groupsDb.SingleOrDefault();

            var group = new Group { Name = groupName };
            var groupNumber = GetGroupNumber(groupName);
            group.DegreeId = groupNumber < 650 ? 1 : 2; // ??? Degree Detection

            Database.Groups.Add(group);
            Database.Save();

            _rowProcessingInfo.ValidRowsCount++;
            _rowProcessingInfo.ProcessedRowsCount++;

            return group;
        }

        private string GetGroupExcel(Stream excelFileStream, int row, int col)
        {
            using (var package = new ExcelPackage(excelFileStream))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var nameCell = worksheet.Cells[row, col].Value;

                _rowProcessingInfo.RowsCount++;

                if (nameCell != null)
                {
                    var name = GetGroupName(nameCell.ToString());
                    if (!string.IsNullOrEmpty(name))
                    {
                        return name;
                    }
                }
            }

            _rowProcessingInfo.ProcessedRowsCount++;
            _rowProcessingInfo.InvalidRowsCount++;
            _rowProcessingInfo.InvalidRowsNumbers.Add(row);

            return null;
        }

        private int GetGroupNumber(string text)
        {
            var regex = new Regex(@"(6\d{2})[\p{IsCyrillic}\p{IsCyrillicSupplement}]{1,3}");
            var match = regex.Match(text);
            return int.Parse(match.Groups[1].Value);
        }

        private string GetGroupName(string text)
        {
            var regex = new Regex(@"6\d{2}[\p{IsCyrillic}\p{IsCyrillicSupplement}]{1,3}");
            var match = regex.Match(text);
            return match.Value;
        }
    }
}

public class RowProcessingResult
{
    public int RowsCount { get; set; }
    public int ProcessedRowsCount { get; set; }
    public int ValidRowsCount { get; set; }
    public int InvalidRowsCount { get; set; }
    public IList<int> InvalidRowsNumbers { get; set; } = new List<int>();
}