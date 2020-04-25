using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        // 
        // GET: /Default/

        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /Default/Welcome/ 

        //[Route("{name}/{fav_number:int?}")]
        public IActionResult Welcome(string name = "Username", int fav_number = 5)
        {
            ViewData["FavouriteNumber"] = fav_number;
            ViewData["Message"] = "Hello, " + name;

            return View();
            //return HtmlEncoder.Default.Encode($"Hello, {name}. Your favourite number is {fav_number}");
        }

        // 
        // GET: /Default/Time
        [Route("Time")]
        public IActionResult Time()
        {
            ViewData["Time"] = GetTime();
            return View();
            //return HtmlEncoder.Default.Encode($"Current time: {GetTime()}");
        }

        // 
        // GET: /Default/GetTime --> will throw exception
        [NonAction]
        public DateTime GetTime()
        {
            return DateTime.Now;
        }

        public IActionResult GetList()
        {
            string[] something = new string[] { "a", "b", "c", "d" };
            return PartialView("_View", something);
        }
    }
}
