using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Web.Mapper.Abstract;

namespace WebApplication1.Controllers
{
    public class RequestApplicationController : Controller
    {
        private readonly IRequestManagementService requestService;

        private readonly IMapTo<RequestDTO, RequestDetailsViewModel> requestMapper;
        public RequestApplicationController(IRequestManagementService requestService,
                                            IMapTo<RequestDTO, RequestDetailsViewModel> requestMapper)
        {
            this.requestService = requestService;
            this.requestMapper = requestMapper;
        }
        // GET: RequestApplication
        public ActionResult Index(int? lastRequestId)
        {
            ViewBag.RequestId = lastRequestId.ToString();
            return View();
        }

        // GET
        public ActionResult Details(int id)
        {
            var request = requestService.GetById(id);
            if (request == null)
                return RedirectToAction(nameof(Index));
            else
            {
                RequestDetailsViewModel requestDetails = requestMapper.MapTo(request);
                return View(requestDetails);
            }
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult Create(RequestViewModel requestVM)
        {
            int requestId = requestService.CreateRequest(requestVM.Subject, requestVM.InitMessage.Text);
           // await Response.WriteAsync($"<script language='javascript'>window.alert('Your request ID is  {requestId}');</script>");
            return RedirectToAction(nameof(Index), new { lastRequestId = requestId });
        }

        // GET
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add new mwssage to request

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}