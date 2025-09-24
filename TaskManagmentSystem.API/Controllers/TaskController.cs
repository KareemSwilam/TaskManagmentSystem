using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dtos.TaskDtos;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;
        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }
        [HttpGet("GetTaskByTitle")]
        public async Task<IActionResult> GetTaskByTitle(string title)
        {
            var result = await _taskServices.GetTask(title);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        
        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskServices.GetAllTasks();
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetTaskWithCreatedUser")]
        public async Task<IActionResult> GetTaskWithCreatedUser(int id)
        {
            var result = await _taskServices.GetTaskWithCreatedUser(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetTaskWithProject")]
        public async Task<IActionResult> GetTaskWithProject(int id)
        {
            var result = await _taskServices.GetTaskWithProject(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetTaskWithTeam")]
        public async Task<IActionResult> GetTaskWithTeam(int id)
        {
            var result = await _taskServices.GetTaskWithTeam(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpGet("GetTaskWithAll")]
        public async Task<IActionResult> GetTaskWithAll(int id)
        {
            var result = await _taskServices.GetTaskWithAll(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] TaskCreateDto createDto)
        {
            var result = await _taskServices.AddTask(createDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpPost("UpdateTask")]    
        public async Task<IActionResult> UpdateTask(string title, [FromBody] TaskUpdateDto updateDto)
        {
            var result = await _taskServices.UpdateTask(title , updateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(string title)
        {
            var result = await _taskServices.DeleteTask(title);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result);
        }
        
    }
}
