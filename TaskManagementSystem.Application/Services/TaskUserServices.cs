using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.TaskDtos;
using TaskManagementSystem.Application.Dtos.TaskUSerDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Services
{
    public class TaskUserServices : ITaskUserServices
    {
        private readonly IUniteOFWork _uniteOFWork;
        public TaskUserServices(IUniteOFWork uniteOFWork)
        {
            _uniteOFWork = uniteOFWork;
        }
        public async Task<CustomResult> AssignTaskToUser(TaskUserCreateDto createDto)
        {
            var userexist = await _uniteOFWork._userRepository.UserExist(createDto.UserEmail);
            if(!userexist) return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var user = await _uniteOFWork._userRepository.GetUser(createDto.UserEmail);
            var taskexist = await _uniteOFWork._tAskRepository.Get(t => t.Id == createDto.TaskId);
            if(taskexist == null) return CustomResult.Failure(CustomError.NotFoundError("Task Not Found"));
            var taskuserexist = await _uniteOFWork._taskUserRepository.Get(tu => tu.TaskId == createDto.TaskId && tu.UserId == user.Id);
            if(taskuserexist != null) return CustomResult.Failure(CustomError.ValidationError("Task Already Assigned To This User"));
            var taskuser = new TaskUser
            {
                TaskId = createDto.TaskId,
                UserId = user.Id,
                Role = createDto.Role,
                Status = "ToDo",
                AssignedAt = DateTime.Now
            };
            await _uniteOFWork._taskUserRepository.Add(taskuser);
            var complete = await _uniteOFWork.SaveChangesAsync();
            if(complete == 0) return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();                        
        }

        public async Task<CustomResult<int>> CountTasksForUser(string userEmail)
        {
            var result  = await GetTaskUsersByUser(userEmail);
            if (!result.IsSuccess)
                return CustomResult<int>.Failure(result.Error);
            var count = result.Value.Count;
            return CustomResult<int>.Success(count);
        }

        public async Task<CustomResult> DeleteTaskToUser(int id)
        {
            var taskuserexist = await _uniteOFWork._taskUserRepository.Get(tu => tu.Id == id);
            if (taskuserexist == null) return CustomResult.Failure(CustomError.ValidationError("Task Not Assigned To This User"));
            await _uniteOFWork._taskUserRepository.Delete(taskuserexist);
            var complete = await _uniteOFWork.SaveChangesAsync();
            if (complete == 0) return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }

        public async Task<CustomResult<List<TaskUserDto>>> GetAllTaskUsers()
        {
            var taskusers = await  _uniteOFWork._taskUserRepository.GetAllTaskUsersWithUserAndTask();
            var taskuserDtos = taskusers.Select(tu => new TaskUserDto
            {
                User = new UserDto
                {
                    FullName = tu.User.FullName,
                    Email = tu.User.Email!,
                    Role = tu.User.Role
                },
                Task = new TaskDto
                {
                    Title = tu.Task.Title,
                    Description = tu.Task.Description,
                },
                DueDate = tu.DueDate,
                Status = tu.Status,
                AssignedAt = tu.AssignedAt,
            }).ToList();
            return CustomResult<List<TaskUserDto>>.Success(taskuserDtos);
        }

        public async Task<CustomResult<TaskUserDto>> GetTaskUser(int id)
        {
            var taskuser = await _uniteOFWork._taskUserRepository.GeTaskUserWithUserAndTask(id);
            var taskuserDto =  new TaskUserDto
            {
                User = new UserDto
                {
                    FullName = taskuser.User.FullName,
                    Email = taskuser.User.Email!,
                    Role = taskuser.User.Role
                },
                Task = new TaskDto
                {
                    Title = taskuser.Task.Title,
                    Description = taskuser.Task.Description,
                },
                DueDate = taskuser.DueDate,
                Status = taskuser.Status,
                AssignedAt = taskuser.AssignedAt,
            };
            return CustomResult<TaskUserDto>.Success(taskuserDto);
        }

        public async Task<CustomResult<List<TaskUserWithUserDto>>> GetTaskUsersByTask(int taskId)
        {
            var taskexist = await _uniteOFWork._tAskRepository.Get(t => t.Id == taskId);    
            if (taskexist == null) return CustomResult<List<TaskUserWithUserDto>>.Failure(CustomError.NotFoundError("Task Not Found"));
            var taskuserexist = await _uniteOFWork._taskUserRepository.Get(tu => tu.TaskId == taskId);
            if (taskuserexist == null) return CustomResult<List<TaskUserWithUserDto>>.Failure(CustomError.NotFoundError("Task Not Assigned To Any User"));
            var taskusers = await _uniteOFWork._taskUserRepository.GetTaskUsersByTaskId(taskId);
            var taskuserDtos = taskusers.Select(tu => new TaskUserWithUserDto
            {
                User = new UserDto
                {
                    FullName = tu.User.FullName,
                    Email = tu.User.Email!,
                    Role = tu.User.Role
                },
                
                DueDate = tu.DueDate,
                Status = tu.Status,
                AssignedAt = tu.AssignedAt,
            }).ToList();
            return CustomResult<List<TaskUserWithUserDto>>.Success(taskuserDtos);
        }

        public async Task<CustomResult<List<TaskUserWithTaskDto>>> GetTaskUsersByUser(string userEmail)
        {
            var userexist = await _uniteOFWork._userRepository.UserExist(userEmail);
            if(!userexist) return CustomResult<List<TaskUserWithTaskDto>>.Failure(CustomError.NotFoundError("User Not Found"));
            var user = await _uniteOFWork._userRepository.GetUser(userEmail);
            var taskuserexist = await _uniteOFWork._taskUserRepository.Get(tu => tu.UserId == user.Id);
            var taskusers = await _uniteOFWork._taskUserRepository.GetTaskUsersByUserId(user.Id);
            var taskuserDtos = taskusers.Select(tu => new TaskUserWithTaskDto
            {
                
                Task = new TaskDto
                {
                    Title = tu.Task.Title,
                    Description = tu.Task.Description,
                },
                DueDate = tu.DueDate,
                Status = tu.Status,
                AssignedAt = tu.AssignedAt,
            }).ToList();
            return CustomResult<List<TaskUserWithTaskDto>>.Success(taskuserDtos);
        }

        public async Task<CustomResult> UpdateTaskStatus(int taskUserId, TaskUserUpdateDto updateDto)
        {
            var taskuserexist = await _uniteOFWork._taskUserRepository.Get(tu => tu.Id == taskUserId);
            if (taskuserexist == null) return CustomResult.Failure(CustomError.ValidationError("Task Not Assigned To This User"));
            taskuserexist.Status = updateDto.Status;
            await _uniteOFWork._taskUserRepository.Update(taskuserexist);
            var complete = await _uniteOFWork.SaveChangesAsync();
            if (complete == 0) return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }
    }
}
