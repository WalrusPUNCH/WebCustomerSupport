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
        private readonly IRequestManagementService requestsManagementService;
        private readonly IMapper mapper;
        public AdminController(ISpecialistManagementService specialistManagementService, IRequestManagementService requestsManagementService, IMapper mapper)
        {
            this.specialistManagementService = specialistManagementService;
            this.mapper = mapper;
            this.requestsManagementService = requestsManagementService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Requests  CRUD
        [HttpGet]
        public IActionResult Requests()
        {
            return View(mapper.Map<IEnumerable<RequestDetailsViewModel>>(requestsManagementService.GetAll()));
        }
        [HttpGet("/Admin/Requests/Edit/{id}")]
        public IActionResult EditRequest(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Requests");
            else
            {
                RequestEditViewModel requestDetails = mapper.Map<RequestEditViewModel>(requestsManagementService.GetById((int)Id));
                if (requestDetails != null)
                {
                    requestDetails.AvailableSpecialists = mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetAll());
                    return View(requestDetails);
                }
                else
                    return RedirectToAction("Requests");
            }
        }
        [HttpPost("/Admin/Requests/Edit/{id}")]
        public IActionResult EditRequest(RequestDetailsViewModel requestDetails)
        {
            requestsManagementService.Update(mapper.Map<RequestDTO>(requestDetails));
            return RedirectToAction("Requests");
        }

        [Route("Admin/Requests/Delete/{id}")]
        public IActionResult DeleteRequest(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Requests));
            else
            {
                requestsManagementService.Delete((int)id);
                return RedirectToAction(nameof(Requests));
            }
        }

        [Route("Admin/Requests/Details/{id}")]
        public IActionResult DetailsRequest(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Requests));
            else
            {
                RequestDetailsViewModel req = mapper.Map<RequestDetailsViewModel>(requestsManagementService.GetById((int)id));
                if (req != null)
                    return View(req);
                else
                    return RedirectToAction(nameof(Requests));
            }
        }
        #endregion
        #region Specialists CRUD
        [HttpGet]
        public IActionResult Specialists()
        {
            return View(mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetAll()));
        }

        [HttpGet("/Admin/Specialists/Add")]
        public IActionResult AddSpecialist()
        {
            return View();
        }

        [HttpPost("/Admin/Specialists/Add")]
        public IActionResult AddSpecialist(SpecialistDTO newSpecialist)
        {
            specialistManagementService.AddSpecialist(newSpecialist);
            return RedirectToAction("Specialists");
        }

        //[HttpGet]
        [HttpGet("/Admin/Specialists/Edit/{id}")]
        public IActionResult EditSpecialist(int? Id)
        {
            if (Id == null)
            {
                Response.StatusCode = 404;
                return null;
            }               
            return View(mapper.Map<SpecialistViewModel>(specialistManagementService.GetSpecialistById((int)Id)));
        }

        //[HttpPost]
        [HttpPost("/Admin/Specialists/Edit/{id}")]
        public IActionResult EditSpecialist(SpecialistViewModel specialist)
        {
            specialistManagementService.Update(mapper.Map<SpecialistDTO>(specialist));
            return RedirectToAction("Specialists");
        }
        [Route("/Admin/Specialists/Delete/{id}")]
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
        #endregion
        #region Statistics
        [HttpGet("/Admin/Specialists/Statistics")]
        public IActionResult Statistics()
        {
            var statsVM = new StatisticsViewModel();
            statsVM.FreeSpecialists = mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetSpecialistsWithNoActiveRequests());
            statsVM.SpecialistsAboveAverage = mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetSpecialistsWithAmountOfRequestsAboveAvarage());
            return View(statsVM);
        }
        #endregion
    }
}