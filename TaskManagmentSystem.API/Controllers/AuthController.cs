using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dtos.AuthDtos;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authservices;
        public AuthController(IAuthServices authservices)
        {
            _authservices = authservices;
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto login)
        {
            var result = await _authservices.Login(login);
            if (result.IsSuccess)
                return Ok(new { token = result.Value });
            return BadRequest(new { error = result.Error });
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Registration(RegistrationDto register)
        {
            var result = await _authservices.Registration(register);
            if (result.IsSuccess)
                return Ok(new { token = result.Value });
            return BadRequest(new { error = result.Error });
        }
    }
}
