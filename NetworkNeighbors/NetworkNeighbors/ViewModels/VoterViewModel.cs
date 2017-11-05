using NetworkNeighbors.Controllers;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace NetworkNeighbors.ViewModels
{
    public class VoterViewModel
    {


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
        public string Action
        {
            get
            {
                Expression<Func<ReferralsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

    }
}