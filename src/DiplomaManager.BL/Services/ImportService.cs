using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DiplomaManager.BLL.Interfaces;
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

        public ImportService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public void ImportStudentsInfo(Stream excelFileStream)
        {
            var excelGroup = GetGroupExcel(excelFileStream, 1, 1);
            ProcessGroup(excelGroup);
            var excelFullNames = GetFullNamesExcel(excelFileStream, 3, 2);
            if (excelFullNames != null)
            {
                var excelFullNamesList = excelFullNames.ToList();
                ProcessFullNames(excelFullNamesList);
            }
        }

        private void ProcessFullNames(IEnumerable<string> fullNames)
        {
            foreach (var fullName in fullNames)
            {
                var peopleNames = GetPeopleNames(fullName).ToList();
                foreach (var pName in peopleNames)
                {
                    var pNamesDb =
                        Database.PeopleNames.Get(new FilterExpression<PeopleName>(pn => pn.Name == pName.Name)).ToList();
                    if (pNamesDb.Count == 0)
                    {
                        pName.CreationDate = DateTime.Now;
                        Database.PeopleNames.Add(pName);
                    }
                }
                Database.Save();
            }
        }

        private IEnumerable<PeopleName> GetPeopleNames(string fullName)
        {
            var fullNameSplit = fullName.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (fullNameSplit.Length != 3) throw new InvalidOperationException("Can't get PeopleNames");

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
                var peopleNames = new List<string>();
                for (int row = rowStart; row <= rowCount; row++)
                {
                    var fio = worksheet.Cells[row, col].Value;
                    if (fio != null)
                    {
                        peopleNames.Add(fio.ToString());
                    }
                }
                return peopleNames;
            }
        }

        private void ProcessGroup(string groupName)
        {
            var groupsDb = Database.Groups.Get(new FilterExpression<Group>(g => g.Name == groupName)).ToList();
            if (groupsDb.Count > 0)
                return;

            var group = new Group { Name = groupName };
            var groupNumber = GetGroupNumber(groupName);
            group.DegreeId = groupNumber < 650 ? 1 : 2; // ??? Degree Detection

            Database.Groups.Add(group);
            Database.Save();
        }

        private string GetGroupExcel(Stream excelFileStream, int row, int col)
        {
            using (var package = new ExcelPackage(excelFileStream))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var nameCell = worksheet.Cells[row, col].Value;
                if (nameCell != null)
                {
                    var name = GetGroupName(nameCell.ToString());
                    if (!string.IsNullOrEmpty(name))
                    {
                        return name;
                    }
                }
            }
            throw new InvalidOperationException("Can't get Group");
        }

        private int GetGroupNumber(string text)
        {
            var regex = new Regex(@"(6\d{2})[а-яА-Я]{1,3}");
            var match = regex.Match(text);
            return int.Parse(match.Groups[1].Value);
        }

        private string GetGroupName(string text)
        {
            var regex = new Regex(@"6\d{2}[а-яА-Я]{1,3}");
            var match = regex.Match(text);
            return match.Value;
        }
    }
}
