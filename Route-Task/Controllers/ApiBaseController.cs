using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Route_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
    }
}
