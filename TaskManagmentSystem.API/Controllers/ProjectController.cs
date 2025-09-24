using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectServices _projectservices;
        public ProjectController(IProjectServices projectservices)
        {
            _projectservices = projectservices; 
        }
        [HttpGet("GetProject")]
        public async Task<IActionResult> GetProject(string name)
        {
            var result = await _projectservices.GetProject(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetProjects()
        {
            var result = await _projectservices.GetProjects();
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetCreatedUserofProjects")]
        public async Task<IActionResult> GetProjectsWithGreatedUser(string name)
        {
            var result = await _projectservices.GetProjectsWithGreatedUser(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetProjectTeams")]
        public async Task<IActionResult> GetProjectTeams(string name)
        {
            var result = await _projectservices.GetProjectTeams(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpPost("AddProject")]    
        public async Task<IActionResult> AddProject([FromBody]ProjectCreateDto projectCreateDto)
        {
            var result = await _projectservices.AddProject(projectCreateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpPost("UpdateProject")]
        public async Task<IActionResult> UpdateProject(string name,[FromBody] ProjectUpdateDto projectUpdateDto)
        {
            var result = await _projectservices.UpdateProject(name, projectUpdateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpDelete("DeleteProject")]
        public async Task<IActionResult> DeleteProject(string name)
        {
            var result = await _projectservices.DeleteProject(name);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
    }
}
