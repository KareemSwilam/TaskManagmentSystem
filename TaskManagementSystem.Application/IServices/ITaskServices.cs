using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.TaskDtos;

namespace TaskManagementSystem.Application.IServices
{
    public interface ITaskServices
    {
        public Task<CustomResult> AddTask(TaskCreateDto createDto);
        public Task<CustomResult<TaskDto>> GetTask(string title);
        public Task<CustomResult<List<TaskDto>>> GetAllTasks();
        public Task<CustomResult<TaskWithUserDto>> GetTaskWithCreatedUser(int id);
        public Task<CustomResult<TaskWithProjectDto>> GetTaskWithProject(int id);
        public Task<CustomResult<TaskWithTeamDto>> GetTaskWithTeam(int id);
        public Task<CustomResult<TaskWithAllDto>> GetTaskWithAll(int id);
        public Task<CustomResult> UpdateTask(string title,TaskUpdateDto updateDto);
        public Task<CustomResult> DeleteTask(string title);
    }
}
