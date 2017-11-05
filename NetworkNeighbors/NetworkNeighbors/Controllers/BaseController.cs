using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Concrete;
using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    public class BaseController : Controller
    {
        public IDataRepository db;

        public BaseController()
        {
            db = new Repository();
        }
    }
}