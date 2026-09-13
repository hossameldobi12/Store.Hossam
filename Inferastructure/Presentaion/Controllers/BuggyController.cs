using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound();
        }
        [HttpGet("servererror")]
        public IActionResult GetServerErrorRequest()  // called when server exception happen
        {
            throw new Exception();
            return Ok();
        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)  // valdiation error
        {
            return BadRequest();
        }
        [HttpGet("uunauthorized")]
        public IActionResult GetUnAuthorizedRequest()
        {
            return Unauthorized();
        }
    }
}
