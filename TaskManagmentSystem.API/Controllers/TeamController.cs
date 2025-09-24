using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Application.Services;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamServices _teamServices;
        public TeamController(ITeamServices teamServices)
        {
            _teamServices = teamServices;
        }
        [HttpGet("GetTeam")]
        public async Task<IActionResult> GetTeam(string name)
        {
            var result = await _teamServices.GetTeam(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetTeams")]
        public async Task<IActionResult> GetTeams()
        {
            var result = await _teamServices.GetTeams();
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetCreatedUserofTeams")]
        public async Task<IActionResult> GetTeamsWithGreatedUser(string name)
        {
            var result = await _teamServices.GetTeamWithGreatedUser(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam([FromBody] TeamCreateDto TeamCreateDto)
        {
            var result = await _teamServices.AddTeam(TeamCreateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpPost("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(string name, [FromBody] TeamUpdateDto TeamUpdateDto)
        {
            var result = await _teamServices.UpdateTeam(name, TeamUpdateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpDelete("DeleteTeam")]
        public async Task<IActionResult> DeletePTeam(string name)
        {
            var result = await _teamServices.DeleteTeam(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
    }
}
