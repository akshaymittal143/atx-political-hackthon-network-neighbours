﻿using System.Web.Mvc;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System;
﻿using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Concrete;
using NetworkNeighbors.Models.Entities;

namespace NetworkNeighbors.Controllers
{
    [Authorize]
    public class ReferralsController : Controller
    {
        private IDataRepository db;

        public ReferralsController()
        {
            db = new Repository();
        }

        [HttpGet]
        public ViewResult CheckRegistration()
        {
            var entity = db.Voters.Where(n => n.voter_id == User.Identity.Name).FirstOrDefault();
            if(entity == null)
            {
                entity = new Voter
                {
                    voter_id = User.Identity.Name
                };
            }
            ViewBag.state = Models.Helpers.GetStateSelectList(entity.state, "--SELECT STATE--");
            return View(entity);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckRegistration(Voter entity)
        {
            entity.voter_id = User.Identity.Name;
            // check van for voter
            var voter = await db.CheckVoterAsync(entity);
            if (voter != null)
            {
                if (!String.IsNullOrEmpty(voter.van_id))
                {
                    return RedirectToAction("Verify", new { id = entity.van_id });
                }
                else
                {
                    return RedirectToAction("RegisterToVote");
                }
            }
            else
            {
                // bad
                ViewBag.state = Models.Helpers.GetStateSelectList(entity.state, "--SELECT STATE--");
                ViewBag.Messages = "Sorry, a problem occurred while checking your registration.";
                return View(entity);
            }
        }

        [Authorize]
        public async Task<ActionResult> Verify()
        {
            var voter = db.Voters.Where(n => n.voter_id == User.Identity.Name).FirstOrDefault();
            var entity = await db.GetPersonInVANAsync(voter.van_id);
            return View(entity);
        }

        [Authorize]
        public ActionResult RegisterToVote()
        {
            return View();
        }
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