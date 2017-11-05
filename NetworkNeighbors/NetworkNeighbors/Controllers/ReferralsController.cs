using NetworkNeighbors.Models;
using NetworkNeighbors.Models.Entities;
using NetworkNeighbors.ViewModels;
using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    public class ReferralsController : Controller
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

        [HttpGet]
        public ViewResult Referral(string userID)
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VoterViewModel viewModel)
        {

            var vote = new Voter
            {
                first_name = viewModel.FirstName,
                last_name = viewModel.LastName,
                dob = viewModel.DOB,
                email = viewModel.Email,
                phone = viewModel.Phone,
                address_1 = viewModel.Address1,
                address_2 = viewModel.Address2,
                city = viewModel.City,
                state = viewModel.State,
                zip_code = viewModel.ZipCode
            };
            _context.Voters.Add(vote);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");

        }
    }
}