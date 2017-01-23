using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using DiplomaManager.BLL.DTOs.RequestDTOs;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaManager.Areas.Admin.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DevelopmentAreaDTO, DevelopmentAreaViewModel>());
            var das = Mapper.Map<IEnumerable<DevelopmentAreaDTO>, 
                IEnumerable<DevelopmentAreaViewModel>>(_requestService.GetDevelopmentAreas());
            return View(das);
        }
    }
}
