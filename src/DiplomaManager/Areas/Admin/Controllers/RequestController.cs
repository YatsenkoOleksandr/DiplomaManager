using System.Collections.Generic;
using AutoMapper;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DevelopmentAreaController : Controller
    {
        private readonly IRequestService _requestService;

        public DevelopmentAreaController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            var dasDtos = _requestService.GetDevelopmentAreas();
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentAreaViewModel>());
            var das = Mapper.Map<IEnumerable<DevelopmentAreaDTO>, IEnumerable<DevelopmentAreaViewModel>>(dasDtos);
            return View(das);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            _requestService.DeleteDevelopmentArea(id.Value);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(DevelopmentAreaViewModel da)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaViewModel, DevelopmentAreaDTO>());
                var daDto = Mapper.Map<DevelopmentAreaViewModel, DevelopmentAreaDTO>(da);
                _requestService.AddDevelopmentArea(daDto);
            }
            return View();
        }
    }
}
