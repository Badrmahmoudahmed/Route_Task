using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route_Task.ErrorHandler;

namespace Route_Task.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            
          return BadRequest(new ApiResponse(code));
           
        }
    }
}
