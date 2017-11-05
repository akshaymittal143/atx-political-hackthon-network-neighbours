using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Concrete;
using System.Web.Http;

namespace NetworkNeighbors.Controllers.API
{
    public class ApiBaseController : ApiController
    {
        public ApiBaseController()
        {
            db = new Repository();
        }

        protected IDataRepository db { get; }
    }
}