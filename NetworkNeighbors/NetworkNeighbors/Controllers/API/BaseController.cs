using System.Web.Http;
using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Concrete;

namespace NetworkNeighbors.Controllers.API
{
    public class BaseController : ApiController
    {
        public BaseController()
        {
            db = new Repository();
        }

        protected IDataRepository db { get; }
    }
}