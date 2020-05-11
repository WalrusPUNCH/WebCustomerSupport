using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using AutoMapper;

using CustomerSupport.BL.Abstract;
using CustomerSupport.BL.DTOs;
using WebApplication1.Models;
using WebApplication1.Mapper.Abstract;
using CustomerSupport.Core.Mapper;

namespace WebApplication1.Controllers
{
    public class RequestApplicationController : Controller
    {
        private readonly IRequestManagementService requestService;
        private readonly IPLMapper customMapper;
        public RequestApplicationController(IRequestManagementService requestService,
                                            IPLMapper newMapper)
        {
            this.requestService = requestService;
            customMapper = newMapper;
        }
        // GET: RequestApplication
        public ActionResult Index()
        {
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
                RequestDetailsViewModel requestDetails = customMapper.MapOne<RequestDetailsViewModel>(request);
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

            //await Response.WriteAsync($"<script language='javascript'>window.alert('Your request ID is  {requestId}');</script>");
            return RedirectToAction(nameof(Index));
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

        // GET
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: RequestApplication/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //         TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}