using System.Web.Mvc;

namespace NetworkNeighbors.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }
    }
}