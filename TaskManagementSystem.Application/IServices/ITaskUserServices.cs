using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.TaskUSerDtos;

namespace TaskManagementSystem.Application.IServices
{
    public interface ITaskUserServices
    {
        public Task<CustomResult> AssignTaskToUser(TaskUserCreateDto createDto);
        public Task<CustomResult> UpdateTaskStatus(int taskUserId ,TaskUserUpdateDto updateDto);
        public Task<CustomResult> DeleteTaskToUser(int id);
        public Task<CustomResult<TaskUserDto>> GetTaskUser(int id);
        public Task<CustomResult<List<TaskUserDto>>> GetAllTaskUsers();
        public Task<CustomResult<List<TaskUserWithTaskDto>>> GetTaskUsersByUser(string userEmail);
        public Task<CustomResult<List<TaskUserWithUserDto>>> GetTaskUsersByTask(int taskId);
        public Task<CustomResult<int>> CountTasksForUser(string userEmail);
    }
}
