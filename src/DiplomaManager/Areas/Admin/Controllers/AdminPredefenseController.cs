using DiplomaManager.BLL.DTOs.PredefenseDTOs;
using DiplomaManager.BLL.Interfaces.PredefenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admins")]
    public class AdminPredefenseController: Controller
    {
        private readonly IAdminPredefenseService _service;

        public AdminPredefenseController(IAdminPredefenseService service)
        {
            _service = service;
        }


        public IActionResult Periods()
        {
            return View();
        }



        public IActionResult CreatePeriod()
        {


            return View();
        }

        [HttpPost]
        public IActionResult GetYears(int? degreeId)
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult CreatePeriod(PredefensePeriodDTO period)
        {

            return RedirectToAction("");
        }
    }
}
