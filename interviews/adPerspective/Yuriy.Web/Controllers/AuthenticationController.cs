using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Yuriy.Web.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] int id)
        {
            return null;
        }
    }
}
