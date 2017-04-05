using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

        public ImportService(IDiplomaManagerUnitOfWork uow)
        {
            Database = uow;
        }

        public void ImportStudentsInfo(Stream excelFileStream)
        {
            var excelGroup = GetGroupExcel(excelFileStream, 1, 1);
            var group = ProcessGroup(excelGroup);
            var excelPNames = GetPeopleNamesExcel(excelFileStream, 3, 2);
            var peopleNames = ProcessPeopleNames(excelPNames);
        }

        private void ProcessStudents(Group group, IEnumerable<PeopleName> peopleNames)
        {
            
        }

        private IEnumerable<PeopleName> ProcessPeopleNames(IEnumerable<string> peopleNames)
        {
            var resPeopleNames = new List<PeopleName>();
            foreach (var pName in peopleNames)
            {
                var pNamesDb =
                    Database.PeopleNames.Get(new FilterExpression<PeopleName>(pn => pn.Name == pName)).ToList();
                if (pNamesDb.Count > 0)
                {
                    resPeopleNames.Add(pNamesDb.Single());
                }
                else
                {
                    var peopleName = new PeopleName {Name = pName};
                    Database.PeopleNames.Add(peopleName);
                    resPeopleNames.Add(peopleName);
                }
            }
            //Database.Save();
            return resPeopleNames;
        }

        private IEnumerable<string> GetPeopleNamesExcel(Stream excelFileStream, int rowStart, int col)
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

        private IEnumerable<PeopleName> GetPeopleNames(string text)
        {
            var fioSplit = text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            if (fioSplit.Length != 3) throw new InvalidOperationException("Can't get PeopleNames");

            var lastName = new PeopleName
            {
                Name = fioSplit[0],
                LocaleId = 1, //Invariant Language
                NameKind = NameKind.LastName
            };
            var firstName = new PeopleName
            {
                Name = fioSplit[1],
                LocaleId = 1, //Invariant Language
                NameKind = NameKind.FirstName
            };
            var patronymic = new PeopleName
            {
                Name = fioSplit[2],
                LocaleId = 1, //Invariant Language
                NameKind = NameKind.Patronymic
            };
            return new[] {firstName, lastName, patronymic};
        }

        private Group ProcessGroup(string groupName)
        {
            var groupsDb = Database.Groups.Get(new FilterExpression<Group>(g => g.Name == groupName)).ToList();
            if (groupsDb.Count > 0)
                return groupsDb.Single();

            var group = new Group { Name = groupName };
            var groupNumber = GetGroupNumber(groupName);
            group.DegreeId = groupNumber < 650 ? 1 : 2; // ??? Degree Detection

            Database.Groups.Add(group);
            Database.Save();
            
            return group;
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
