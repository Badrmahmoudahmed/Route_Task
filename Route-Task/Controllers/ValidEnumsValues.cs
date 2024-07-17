using Microsoft.AspNetCore.Mvc;
using Route_Task.Helpers;

namespace Route_Task.Controllers
{
    public class ValidEnumsValues : ApiBaseController
    {
        [HttpGet("Enums Values")]
        public ActionResult<EnumValuesToReturn> GetEnumsValues()
        {

            return Ok(new EnumValuesToReturn());
        }
    }
}
