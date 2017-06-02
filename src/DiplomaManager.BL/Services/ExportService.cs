using System.Collections.Generic;
using System.IO;
using DiplomaManager.BLL.Extensions.Admin;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DiplomaManager.BLL.Services
{
    public class ExportService : IExportService
    {
        public Stream GetTeacherStudentsStream(IEnumerable<TeacherStudents> teacherStudents)
        {
            var excelFileStream = new MemoryStream();
            using (var package = new ExcelPackage(excelFileStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Дипломники");

                worksheet.Row(1).Height = 20;
                worksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Cells[1, 1].Value = "Руководитель";
                worksheet.Cells[1, 2].Value = "Студент";

                var counter = 2;
                foreach (var ts in teacherStudents)
                {
                    worksheet.Cells[counter, 1].Value = ts.Teacher;
                    counter++;
                    foreach (var student in ts.Students)
                    {
                        worksheet.Cells[counter, 2].Value = student;
                        counter++;
                    }
                }

                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();

                package.Save();
                excelFileStream.Seek(0, 0);
            }
            return excelFileStream;
        }
    }
}
