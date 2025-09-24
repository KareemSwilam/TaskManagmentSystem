using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dtos.TeamUserDtos;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUserController : ControllerBase
    {
        private readonly ITeamUserServices _teamUserServices;
        public TeamUserController(ITeamUserServices teamUserServices)
        {
            _teamUserServices = teamUserServices;
        }
        [HttpGet("GetUsersByTeam")]
        public async Task<IActionResult> GetUsersByTeam(int teamId)
        {
            var result = await _teamUserServices.GetUsersByTeam(teamId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpGet("GetTeamsByUser")]
        public async Task<IActionResult> GetTeamsByUser(string userEmail)
        {
            var result = await _teamUserServices.GetTeamsByUser(userEmail);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpGet("GetAllTeamUsers")]
        public async Task<IActionResult> GetAllTeamUsers()
        {
            var result = await _teamUserServices.GetAllTeamUsers();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpGet("CountUsersInTeam")]
        public async Task<IActionResult> CountUsersInTeam(int teamId)
        {
            var result = await _teamUserServices.CountUsersInTeam(teamId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpGet("CountTeamsForUser")]
        public async Task<IActionResult> CountTeamsForUser(string Email)
        {
            var result = await _teamUserServices.CountTeamsForUser(Email);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpPost("AddUserToTeam")]
        public async Task<IActionResult> AddUserToTeam([FromBody] TeamUserCreateDto createDto)
        {
            var result = await _teamUserServices.AddUserToTeam(createDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpPost("UpdateUserRoleinTeam")]
        public async Task<IActionResult> UpdateUserRoleinTeam([FromBody] TeamUserCreateDto createDto)
        {
            var result = await _teamUserServices.UpdateUserRoleinTeam(createDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }
        [HttpDelete("RemoveUserFromTeam")]
        public async Task<IActionResult> RemoveUserFromTeam([FromBody] TeamUserCreateDto createDto)
        {
            var result = await _teamUserServices.RemoveUserFromTeam(createDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Error.ToString());
        }

    }
}
