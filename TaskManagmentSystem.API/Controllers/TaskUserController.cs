using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dtos.TaskUSerDtos;
using TaskManagementSystem.Application.IServices;

namespace TaskManagmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskUserController : ControllerBase
    {
        private readonly ITaskUserServices _taskUserServices;
        public TaskUserController(ITaskUserServices taskUserServices)
        {
            _taskUserServices = taskUserServices;
        }
        [HttpGet("GetTaskUser")]
        public async Task<IActionResult> GetTaskUser(int id)
        {
            var result = await _taskUserServices.GetTaskUser(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result.Value);
        }
        [HttpGet("GetAllTaskUsers")]
        public async Task<IActionResult> GetAllTaskUsers()
        {
            var result = await _taskUserServices.GetAllTaskUsers();
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result.Value);
        }
        [HttpGet("GetTaskUsersByUser")]
        public async Task<IActionResult> GetTaskUsersByUser(string userEmail)
        {
            var result = await _taskUserServices.GetTaskUsersByUser(userEmail);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result.Value);
        }
        [HttpGet("GetTaskUsersByTask")]
        public async Task<IActionResult> GetTaskUsersByTask(int taskId)
        {
            var result = await _taskUserServices.GetTaskUsersByTask(taskId);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result.Value);
        }
        [HttpGet("CountTasksForUser")]
        public async Task<IActionResult> CountTasksForUser(string userEmail)
        {
            var result = await _taskUserServices.CountTasksForUser(userEmail);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok(result.Value);
        }
        [HttpPost("AssignTaskToUser")]
        public async Task<IActionResult> AssignTaskToUser([FromBody] TaskUserCreateDto createDto)
        {
            var result = await _taskUserServices.AssignTaskToUser(createDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok();
        }
        [HttpPost("UpdateTaskStatus")]
        public async Task<IActionResult> UpdateTaskStatus(int taskUserId, [FromBody] TaskUserUpdateDto updateDto)
        {
            var result = await _taskUserServices.UpdateTaskStatus(taskUserId, updateDto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok();
        }
        [HttpDelete("DeleteTaskToUser")]
        public async Task<IActionResult> DeleteTaskToUser(int id)
        {
            var result = await _taskUserServices.DeleteTaskToUser(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error.ToString());
            return Ok();
        }
    }
}
