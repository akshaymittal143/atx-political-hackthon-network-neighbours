using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    public class ReferralsController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Referral(string userID)
        {
            return View();
        }
    }
}