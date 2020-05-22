using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Controllers

{
    public class AdminController : Controller
    {
        private readonly ISpecialistManagementService specialistManagementService;
        private readonly IRequestManagementService requestsManagementService;

        private readonly IMapTo<RequestDTO, RequestDetailsViewModel> requestDetailsMapper;
        private readonly IMap<RequestDTO, RequestEditViewModel> requestEditMapper;
        private readonly IMap<SpecialistDTO, SpecialistViewModel> specialistMapper;

        public AdminController(ISpecialistManagementService specialistManagementService,
                               IRequestManagementService requestsManagementService,
                               IMapTo<RequestDTO, RequestDetailsViewModel>  requestDetailsMapper,
                               IMap<RequestDTO, RequestEditViewModel> requestEditMapper,
                               IMap<SpecialistDTO, SpecialistViewModel> specialistMapper)
        {
            this.specialistManagementService = specialistManagementService;
            this.requestsManagementService = requestsManagementService;

            this.requestDetailsMapper = requestDetailsMapper;
            this.requestEditMapper = requestEditMapper;
            this.specialistMapper = specialistMapper;
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
                Elements = requestsManagementService.GetAll(page, _pageSize).Select(r => requestDetailsMapper.MapTo(r)).ToList()
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
                RequestEditViewModel requestDetails = requestEditMapper.MapTo(requestsManagementService.GetById((int)Id));
                if (requestDetails != null)
                {
                    requestDetails.AvailableSpecialists = specialistManagementService.GetAll().Select(s => specialistMapper.MapTo(s));
                    return View(requestDetails);
                }
                else
                    return RedirectToAction("Requests");
            }
        }
        [HttpPost("/Admin/Requests/Edit/{id}")]
        public IActionResult EditRequest(RequestEditViewModel requestDetails)
        {
            requestsManagementService.Update(requestEditMapper.MapFrom(requestDetails));
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
                RequestDetailsViewModel req = requestDetailsMapper.MapTo(requestsManagementService.GetById((int)id));
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
                Elements = specialistManagementService.GetAll(page, _pageSize).Select(s => specialistMapper.MapTo(s)).ToList()
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
            return View(specialistMapper.MapTo(specialistManagementService.GetById((int)Id)));
        }

        //[HttpPost]
        [HttpPost("/Admin/Specialists/Edit/{id}")]
        public IActionResult EditSpecialist(SpecialistViewModel specialist)
        {
            specialistManagementService.Update(specialistMapper.MapFrom(specialist));
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
            statsVM.FreeSpecialists = specialistManagementService.GetSpecialistsWithNoActiveRequests().Select(s => specialistMapper.MapTo(s)); ;
            statsVM.SpecialistsAboveAverage = specialistManagementService.GetSpecialistsWithAmountOfRequestsAboveAvarage().Select(s => specialistMapper.MapTo(s)); ;
            return View(statsVM);
        }
        #endregion
    }
}