using System.Threading.Tasks;
using System.Web.Http;

namespace NetworkNeighbors.Controllers.API
{
    [RoutePrefix("api/Van")]
    public class VanController : ApiBaseController
    {
        [Route("Echo")]
        [HttpGet]
        public async Task<IHttpActionResult> Echo(string str)
        {
            return Ok(await db.VANEchoAsync(str));
        }
    }
}