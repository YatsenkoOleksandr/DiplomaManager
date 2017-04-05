using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiplomaManager.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
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

        public IActionResult GetData()
        {
            string sWebRootFolder = HostingEnvironment.WebRootPath;
            string sFileName = @"list__631pst.xlsx";

            var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            ImportService.ImportStudentsInfo(fs);

            return View("Index");
        }
    }
}
