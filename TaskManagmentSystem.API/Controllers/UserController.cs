using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;   
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(string Email)
        {
            var result = await _userServices.GetUser(Email);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userServices.GetUsers();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetProjectGreatedByUser")]
        public async Task<IActionResult> GetProjectGreatedByUser(string Email)
        {
            var result = await _userServices.GetProjectGreatedByUser(Email);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("GetTeamGreatedByUser")]
        public async Task<IActionResult> GetTeamGreatedByUser(string Email)
        {
            var result = await _userServices.GetTeamGreatedByUser(Email);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserCreateDto userCreateDto)
        {
            var result = await _userServices.AddUser(userCreateDto);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result.Error.ToString());
        }
        
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var result = await _userServices.ChangePassword(request);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string Email)
        {
            var result = await _userServices.DeleteUser(Email);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
