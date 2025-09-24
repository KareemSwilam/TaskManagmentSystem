using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetUpController : ControllerBase
    {
        private readonly ISetupServices _setupServices;
        public SetUpController(ISetupServices setupServices)
        {
            _setupServices = setupServices;
        }
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string name)
        {
            var result = await _setupServices.AddRole(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }
        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string userEmail, string roleName)
        {
            var result = await _setupServices.AddRoleToUser(userEmail, roleName);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }
        [HttpGet("CheckUserInRole")]
        public async Task<IActionResult> CheckUserInRole(string userEmail, string role)
        {
            var result = await _setupServices.CheckUserInRole(userEmail, role);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _setupServices.GetRoles();
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles( string userEmail)
        {
            var result = await _setupServices.GetUserRoles(userEmail);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string userEmail, string roleName)
        {
            var result = await _setupServices.RemoveRoleFromUser(userEmail, roleName);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }
        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> RemoveRole(string name)
        {
            var result = await _setupServices.RemoveRole(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }
    }
}
