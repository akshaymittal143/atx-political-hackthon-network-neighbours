using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Concrete;
using NetworkNeighbors.Models.Entities;
using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    public class ReferralsController : Controller
    {
        private IDataRepository db;

        public ReferralsController()
        {
            db = new Repository();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CheckRegistration()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckRegistration(Voter entity)
        {
            string voter_id = db.SaveVoter(entity);
            if (!string.IsNullOrEmpty(voter_id))
            {
                // good
            }
            else
            {
                // bad
            }

            return RedirectToAction("Mine", "Gigs");

        }

        [HttpGet]
        public ViewResult Referral(string userID)
        {
            return View();
        }

    }
}