using NetworkNeighbors.Models;
using NetworkNeighbors.Models.Entities;
using NetworkNeighbors.ViewModels;
using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    public class ReferralsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public ReferralsController()
        {
            _context = new ApplicationDbContext();
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