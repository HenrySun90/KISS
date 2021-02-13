using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kiss.Models.System;
using Kiss.Services.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kiss.WebApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        public AuthController(AuthService AuthService)
        {
            _authService = AuthService;
        }

        [HttpPost("GetToken")]
        public async Task<ActionResult> GetToken(UserLoginDto dto, [FromQuery]bool force=false)
        {
            try
            {
                var token = await _authService.Login(dto, force);
                return Ok(new { Code = 0, Msg = "OK", Data = token });
            }
            catch (Exception e)
            {

                return Ok(new { Code = 1, Msg = $"Error: {e.Message}" });
            }
           
        }
    }
}
