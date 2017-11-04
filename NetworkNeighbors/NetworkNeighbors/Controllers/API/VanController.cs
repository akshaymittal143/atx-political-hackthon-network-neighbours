using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetworkNeighbors.Controllers.API
{
    public class VanController : ApiController
    {
        [HttpGet]
        public string Echo (string str)
        {
            return Models.VAN.Echo(str);
        }
    }
}
