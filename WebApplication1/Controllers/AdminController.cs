using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;


using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.DTOs;

using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Mapper.Abstract;
using System.Runtime.CompilerServices;
using CustomerSupport.Core.Mapper;

namespace WebApplication1.Controllers

{
    public class AdminController : Controller
    {
        private readonly ISpecialistManagementService specialistManagementService;
        private readonly IRequestManagementService requestsManagementService;
        private readonly IPLMapper customMapper;
        public AdminController(ISpecialistManagementService specialistManagementService,
                               IRequestManagementService requestsManagementService, 
                               IPLMapper customMapper)
        {
            this.specialistManagementService = specialistManagementService;
            this.customMapper = customMapper;
            this.requestsManagementService = requestsManagementService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Requests  CRUD
        [HttpGet]
        public IActionResult Requests(int? pageSize, int page = 1)
        {
            int _pageSize = pageSize ?? 10;
            PagedResult<RequestDetailsViewModel> pagedResult = new PagedResult<RequestDetailsViewModel>
            {
                CurrentPage = page,
                PageSize = _pageSize,
                PageCount = (int)Math.Ceiling(requestsManagementService.Count() / (double)_pageSize),
                Elements = customMapper.MapMany<RequestDetailsViewModel>(requestsManagementService.GetAll(page, _pageSize)).ToList()
            };

            return View(pagedResult);
           // return View(mapper.Map<IEnumerable<RequestDetailsViewModel>>(requestsManagementService.GetAll()));
        }
        [HttpGet("/Admin/Requests/Edit/{id}")]
        public IActionResult EditRequest(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Requests");
            else
            {
                RequestEditViewModel requestDetails = customMapper.MapOne<RequestEditViewModel>(requestsManagementService.GetById((int)Id));
                if (requestDetails != null)
                {
                    requestDetails.AvailableSpecialists = customMapper.MapMany<SpecialistViewModel>(specialistManagementService.GetAll());
                    return View(requestDetails);
                }
                else
                    return RedirectToAction("Requests");
            }
        }
        [HttpPost("/Admin/Requests/Edit/{id}")]
        public IActionResult EditRequest(RequestEditViewModel requestDetails)
        {
            requestsManagementService.Update(customMapper.MapOne<RequestDTO>(requestDetails));
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
                RequestDetailsViewModel req = customMapper.MapOne<RequestDetailsViewModel>(requestsManagementService.GetById((int)id));
                if (req != null)
                    return View(req);
                else
                    return RedirectToAction(nameof(Requests));
            }
        }
        #endregion
        #region Specialists CRUD
        [HttpGet]
        public IActionResult Specialists(int? pageSize, int page = 1)
        {
            int _pageSize = pageSize ?? 1;
            PagedResult<SpecialistViewModel> pagedResult = new PagedResult<SpecialistViewModel>
            {
                CurrentPage = page,
                PageSize = _pageSize,
                PageCount = (int)Math.Ceiling(specialistManagementService.Count() / (double)_pageSize),
                Elements = customMapper.MapMany<SpecialistViewModel>(specialistManagementService.GetAll(page, _pageSize)).ToList()
            };

            return View(pagedResult);
            //return View(mapper.Map<IEnumerable<SpecialistViewModel>>(specialistManagementService.GetAll()));
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
            return View(customMapper.MapOne<SpecialistViewModel>(specialistManagementService.GetSpecialistById((int)Id)));
        }

        //[HttpPost]
        [HttpPost("/Admin/Specialists/Edit/{id}")]
        public IActionResult EditSpecialist(SpecialistViewModel specialist)
        {
            specialistManagementService.Update(customMapper.MapOne<SpecialistDTO>(specialist));
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
            specialistManagementService.Delete((int)id);
            return RedirectToAction("Specialists");
            
        }
        #endregion
        #region Statistics
        [HttpGet("/Admin/Specialists/Statistics")]
        public IActionResult Statistics()
        {
            var statsVM = new StatisticsViewModel();
            statsVM.FreeSpecialists = customMapper.MapMany<SpecialistViewModel>(specialistManagementService.GetSpecialistsWithNoActiveRequests());
            statsVM.SpecialistsAboveAverage = customMapper.MapMany<SpecialistViewModel>(specialistManagementService.GetSpecialistsWithAmountOfRequestsAboveAvarage());
            return View(statsVM);
        }
        #endregion
    }
}