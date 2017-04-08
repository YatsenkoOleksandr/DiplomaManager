using System;
using System.Collections.Generic;
using System.IO;
using DiplomaManager.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImportController : Controller
    {
        private IImportService ImportService { get; }
        private IHostingEnvironment HostingEnvironment { get; }

        public ImportController(IImportService importService, IHostingEnvironment hostingEnvironment)
        {
            ImportService = importService;
            HostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportData(ICollection<IFormFile> files)
        {
            var filesProcessingInfo = new List<FilesProcessingResult>();
            FilesProcessingResult.FilesCount = files.Count;

            var uploads = Path.Combine(HostingEnvironment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                var fileProcessingInfo = new FilesProcessingResult {FileInfo = new FileInfo(file.FileName)};
                try
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.Combine(uploads, file.FileName);
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            fileProcessingInfo.RowProcessingResult = ImportService.ImportStudentsInfo(fileStream);
                        }
                        fileProcessingInfo.IsValid = true;
                    }
                }
                catch (Exception ex)
                {
                    fileProcessingInfo.IsValid = false;
                }
                finally
                {
                    filesProcessingInfo.Add(fileProcessingInfo);
                }
            }
            return View(filesProcessingInfo);
        }
    }

    public class FilesProcessingResult
    {
        public FileInfo FileInfo { get; set; }
        public RowProcessingResult RowProcessingResult { get; set; }

        public static int FilesCount { get; set; }

        public bool IsValid { get; set; }
    }
}
