using FirstApplication_V2.Models.filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstApplication_V2.Controllers
{
    [MyLogActionFilter]

   public class FilterTestController : Controller
    {
        // GET: Home

        [OutputCache(Duration = 15)]
        public string Index()
        {
            return "This is ASP.Net MVC Filters Tutorial";
        }

        [OutputCache(Duration = 20)]
        [ActionName("CurrentTime")]
        public string GetCurrentTime()
        {
            //return DateTime.Now.ToString();
            return TimeString();
        }
        [NonAction]
        public string TimeString()
        {
            return "Time is " + DateTime.Now.ToString("T");
        }


    }
}