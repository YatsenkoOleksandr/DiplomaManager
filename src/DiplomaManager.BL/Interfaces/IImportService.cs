using System.IO;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IImportService
    {
        RowProcessingResult ImportStudentsInfo(Stream excelFileStream);
    }
}
