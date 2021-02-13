using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kiss.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        public IActionResult Demo()
        {
            string username = HttpContext.User.Identity.Name;
            string role = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
            return Ok(new { Code = 1, UserName = username,Role=role });
        }
    }
}
