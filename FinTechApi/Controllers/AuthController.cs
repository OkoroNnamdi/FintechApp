using FinTech.DB.DTO;
using FinTechCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTechApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAuthenticationService _service;
        public AuthController(IAuthenticationService service)
        {
            _service = service;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var register = await _service.Register(user);
            if (register.Succeeded == true) return Ok(register);
            return Ok(register);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var login = await _service.Login(model);
            if (login.Succeeded == false) return BadRequest(login);
            return Ok(login);
        }
        [Authorize]
        [HttpGet("Refresh-Token")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = await _service.RefreshToken();
            return Ok(token);
        }
        [Authorize]
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var result = await _service.ChangePassword(model);
            return Ok(result);
        }
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var result = await _service.ForgottenPassword(model);
            return Ok(result);
        }
        [HttpPost("Reset-Update-Password")]
        public async Task<IActionResult> ResetUpdatePassword([FromBody] UpdatePasswordDTO model)
        {
            var result = await _service.ResetPassword(model );
            return Ok(result);
        }
        [HttpPut("signout")]
        public async Task<IActionResult> Signout()
        {
            await _service.Signout();
            return Ok();
        }
    }
    
}


