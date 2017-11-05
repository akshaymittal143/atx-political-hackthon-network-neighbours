using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    [RoutePrefix("Planning")]
    public class PlanningController : Controller
    {
        [Route]
        [Route("~")]
        [Route("Index")]
        public ViewResult Index()
        {
            return View();
        }
    }
}