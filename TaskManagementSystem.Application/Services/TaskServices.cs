using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TaskDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Services
{
    public class TaskServices : ITaskServices
    {
        private readonly IUniteOFWork _uniteofwork;
        public TaskServices(IUniteOFWork uniteofwork)
        {
            _uniteofwork = uniteofwork;
        }
        public async  Task<CustomResult> AddTask(TaskCreateDto createDto)
        {
            if(createDto == null) 
                return CustomResult.Failure(CustomError.ValidationError("CreateDto is null")); 
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Id == createDto.ProjectId);
            if(projectexist == null)
                 return CustomResult.Failure(CustomError.NotFoundError("Project Not Found"));
            var teamexist = await _uniteofwork._teamRepository.Get(p => p.Id == createDto.TeamId);
            if (projectexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Team Not Found"));
            var userexist = await _uniteofwork._userRepository.UserExist(createDto.CreatedUserEmail);
            if (!userexist)
                return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var user = await _uniteofwork._userRepository.GetUser(createDto.CreatedUserEmail);
            var task = new TAsk
            {
                Title = createDto.Title,
                Description = createDto.Description,
                ProjectId = createDto.ProjectId,
                TeamId = createDto.TeamId,
                CreatedUserId = user.Id,
            };
            await _uniteofwork._tAskRepository.Add(task);
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }

        public async Task<CustomResult> DeleteTask(string title)
        {
            var taskexist = await _uniteofwork._tAskRepository.Get(t => t.Title == title);  
            if(taskexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Task Not Found"));
            await _uniteofwork._tAskRepository.Delete(taskexist);
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();

        }

        public async Task<CustomResult<List<TaskDto>>> GetAllTasks()
        {
            var tasks = await  _uniteofwork._tAskRepository.GetAllAsync();
            var tasksdto = tasks.Select(t => new TaskDto
            {
                Title = t.Title,
                Description = t.Description,
                
            }).ToList();
            return CustomResult<List<TaskDto>>.Success(tasksdto);
        }

        public async Task<CustomResult<TaskDto>> GetTask(string title)
        {
            var task = await  _uniteofwork._tAskRepository.Get(t => t.Title == title);
            var taskdto = new TaskDto
            {
                Title = task.Title,
                Description = task.Description,

            };
            return CustomResult<TaskDto>.Success(taskdto);
        }

        public async Task<CustomResult<TaskWithAllDto>> GetTaskWithAll(int id)
        {
            var taskexist = await _uniteofwork._tAskRepository.Get(t => t.Id == id);
            if (taskexist == null)
                return CustomResult<TaskWithAllDto>.Failure(CustomError.NotFoundError("Task Not Found"));
            var task = await _uniteofwork._tAskRepository.GetTaskWithAll(id);
            var result = new TaskWithAllDto
            {
                Title = task.Title,
                Description = task.Description,
                CreatedUser = new UserDto
                {
                    FullName = task.CreatedUser.FullName,
                    Role = task.CreatedUser.Role,
                    Email = task.CreatedUser.Email!
                },
                Team = new TeamDto
                {
                    Name = task.Team.Name
                },
                Project = new ProjectDto
                {
                    Name = task.Project.Name,
                    Description = task.Project.Description
                }
            };
            return CustomResult<TaskWithAllDto>.Success(result);

        }

        public async Task<CustomResult<TaskWithUserDto>> GetTaskWithCreatedUser(int id)
        {
            var taskexist = await _uniteofwork._tAskRepository.Get(t => t.Id == id);
            if (taskexist == null)
                return CustomResult<TaskWithUserDto>.Failure(CustomError.NotFoundError("Task Not Found"));
            var task = await _uniteofwork._tAskRepository.GetTaskWithCreatedUser(id);
            var result = new TaskWithUserDto
            {
                Title = task.Title,
                Description = task.Description,
                CreatedUser = new UserDto
                {
                    FullName = task.CreatedUser.FullName,
                    Role = task.CreatedUser.Role,
                    Email = task.CreatedUser.Email!
                },
                
            };
            return CustomResult<TaskWithUserDto>.Success(result);
        }

        public async Task<CustomResult<TaskWithProjectDto>> GetTaskWithProject(int id)
        {
            var taskexist = await _uniteofwork._tAskRepository.Get(t => t.Id == id);
            if (taskexist == null)
                return CustomResult<TaskWithProjectDto>.Failure(CustomError.NotFoundError("Task Not Found"));
            var task = await _uniteofwork._tAskRepository.GetTaskWithProject(id);
            var result = new TaskWithProjectDto
            {
                Title = task.Title,
                Description = task.Description,
                
                Project = new ProjectDto
                {
                    Name = task.Project.Name,
                    Description = task.Project.Description
                }
            };
            return CustomResult<TaskWithProjectDto>.Success(result);
        }

        public async Task<CustomResult<TaskWithTeamDto>> GetTaskWithTeam(int id)
        {
            var taskexist = await _uniteofwork._tAskRepository.Get(t => t.Id == id);
            if (taskexist == null)
                return CustomResult<TaskWithTeamDto>.Failure(CustomError.NotFoundError("Task Not Found"));
            var task = await _uniteofwork._tAskRepository.GetTaskWithTeam(id);
            var result = new TaskWithTeamDto
            {
                Title = task.Title,
                Description = task.Description,
                Team = new TeamDto
                {
                    Name = task.Team.Name
                },
                
            };
            return CustomResult<TaskWithTeamDto>.Success(result);
        }

        public async  Task<CustomResult> UpdateTask(string title, TaskUpdateDto updateDto)
        {
            if (updateDto == null)
                return CustomResult.Failure(CustomError.ValidationError("CreateDto is null"));
            var taskexist = await _uniteofwork._tAskRepository.Get(t => t.Title == title);
            if (taskexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Task Not Found"));
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Id == updateDto.ProjectId);
            if (projectexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Project Not Found"));
            var teamexist = await _uniteofwork._teamRepository.Get(p => p.Id == updateDto.TeamId);
            if (projectexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Team Not Found"));
            var userexist = await _uniteofwork._userRepository.UserExist(updateDto.CreatedUserEmail);
            if (!userexist)
                return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var user = await _uniteofwork._userRepository.GetUser(updateDto.CreatedUserEmail);
            
            taskexist.Title = updateDto.Title;
            taskexist.Description = updateDto.Description;
            taskexist.ProjectId = updateDto.ProjectId;
            taskexist.TeamId = updateDto.TeamId;
            taskexist.CreatedUserId = user.Id;
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }
    }
}
