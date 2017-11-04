using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Concrete;

namespace NetworkNeighbors.Controllers.API
{
    public class BaseController : ApiController
    {
        public IDataRepository db;

        public BaseController()
        {
            db = new Repository();
        }
    }
}
