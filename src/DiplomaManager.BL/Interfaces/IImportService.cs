using System.IO;

namespace DiplomaManager.BLL.Interfaces
{
    public interface IImportService
    {
        void ImportStudentsInfo(Stream excelFileStream);
    }
}
