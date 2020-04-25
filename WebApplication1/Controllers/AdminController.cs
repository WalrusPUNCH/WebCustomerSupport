using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;


using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.DTOs;

using WebApplication1.Models;

namespace WebApplication1.Controllers

{
    public class AdminController : Controller
    {
        private readonly ISpecialistManagementService specialistManagementService;
        private readonly IRequestInfoService requestsInfoService;
        private readonly IMapper mapper;
        public AdminController(ISpecialistManagementService specialistManagementService, IRequestInfoService requestsInfo, IMapper mapper)
        {
            this.specialistManagementService = specialistManagementService;
           // statisticsService = statsService;
            this.mapper = mapper;
            requestsInfoService = requestsInfo;
        }

        public IActionResult Specialists()
        {
                return View(mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetAll()));
        }

        public IActionResult Requests()
        {
            return View(mapper.Map<IEnumerable<RequestDetailsViewModel>>(requestsInfoService.GetAll()));
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSpecialist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSpecialist(SpecialistDTO newSpecialist)
        {
            specialistManagementService.AddSpecialist(newSpecialist);
            return RedirectToAction("Specialists");
        }

        [HttpGet]
        public IActionResult EditSpecialist(int? Id)
        {
            if (Id == null)
            {
                Response.StatusCode = 404;
                return null;
            }               
            return View(mapper.Map<SpecialistViewModel>(specialistManagementService.GetSpecialistById((int)Id)));
        }

        [HttpPost]
        public IActionResult EditSpecialist(SpecialistViewModel specialist)
        {
            specialistManagementService.Update(mapper.Map<SpecialistDTO>(specialist));
            return RedirectToAction("Specialists");
        }

        public IActionResult DeleteSpecialist(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            if (specialistManagementService.Delete((int)id) == false)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
                return RedirectToAction("Specialists");
            
        }

        public IActionResult Statistics()
        {
            var statsVM = new StatisticsViewModel();
            statsVM.FreeSpecialists = mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetSpecialistsWithNoActiveRequests());
            statsVM.SpecialistsAboveAverage = mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetSpecialistsWithAmountOfRequestsAboveAvarage());
            return View(statsVM);
        }
    }
}