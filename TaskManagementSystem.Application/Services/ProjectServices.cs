using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly IUniteOFWork _uniteofwork;
        public ProjectServices(IUniteOFWork uniteofwork)
        {
            _uniteofwork = uniteofwork;
        }
        public async Task<CustomResult> AddProject(ProjectCreateDto projectCreateDto)
        {
            if (projectCreateDto == null)
                return  CustomResult.Failure(CustomError.ValidationError("InValid Input"));
            var userexist = await _uniteofwork._userRepository.UserExist(projectCreateDto.CreatedByUserEmail); 
            if (!userexist)
                return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var user = await _uniteofwork._userRepository.GetUser( projectCreateDto.CreatedByUserEmail);
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Name == projectCreateDto.Name);
            if (projectexist != null)
                return CustomResult.Failure(CustomError.ValidationError("Project Name Already Exist"));
            var project = new Project
            {
                Name = projectCreateDto.Name,
                Description = projectCreateDto.Description,
                CreatedUserId = user.Id
            };
            await _uniteofwork._projectRepository.Add(project);
            var complete = await _uniteofwork.SaveChangesAsync();
            if(complete == 0 )
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();

        }

        public async  Task<CustomResult> DeleteProject(string name)
        {
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Name == name);
            if (projectexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Project Not Found "));
            await _uniteofwork._projectRepository.Delete(projectexist);
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }

        public async Task<CustomResult<ProjectDto>> GetProject(string name)
        {
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Name == name);
            if (projectexist == null)
                return CustomResult<ProjectDto>.Failure(CustomError.NotFoundError("Project Not Found "));
            var projectdto = new ProjectDto
            {
                Name = projectexist.Name,
                Description = projectexist.Description,
            };
            return CustomResult<ProjectDto>.Success(projectdto);

        }

        public async Task<CustomResult<List<ProjectDto>>> GetProjects()
        {
            var projectexist = await _uniteofwork._projectRepository.GetAllAsync();
            if (projectexist == null)
                return CustomResult<List<ProjectDto>>.Failure(CustomError.NotFoundError("Project Not Found "));
            var projectdtos = new List<ProjectDto>();
            foreach( var project in projectexist )
            {
                var projectdto = new ProjectDto
                {
                    Name = project.Name,
                    Description = project.Description,
                };
                projectdtos.Add(projectdto);
            }
            return CustomResult<List<ProjectDto>>.Success(projectdtos);
        }

        public async Task<CustomResult<ProjectWithCreatedUserDto>> GetProjectsWithGreatedUser(string name)
        {
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Name == name);
            if (projectexist == null)
                return CustomResult<ProjectWithCreatedUserDto>.Failure(CustomError.NotFoundError("Project Not Found "));
            var createduser = await _uniteofwork._projectRepository.GetCreatedUser(name);
            var projectdto = new ProjectWithCreatedUserDto
            {
                Name = projectexist.Name,
                Description = projectexist.Description,
                CreatedUser = new UserDto
                {
                    Email = createduser.Email!,
                    FullName = createduser.FullName,
                    Role = createduser.Role
                }
            };
            return CustomResult<ProjectWithCreatedUserDto>.Success(projectdto);
        }

        public async Task<CustomResult<List<TeamDto>>> GetProjectTeams(string name)
        {
            var projectExist = await _uniteofwork._projectRepository.Get(p => p.Name == name);
            if (projectExist == null)
                return CustomResult<List<TeamDto>>.Failure(CustomError.NotFoundError("Project Not Found"));
            var teams = await _uniteofwork._projectRepository.GetProjectteams(name);
            var teamsdto = teams.Select(t => new TeamDto
            {
                Name = t.Name,
            }).ToList();
            return CustomResult<List<TeamDto>>.Success(teamsdto);
        }

        public async Task<CustomResult> UpdateProject(string name, ProjectUpdateDto projectUpdateDto)
        {
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Name == name);
            if (projectexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Project Not Found "));
            projectexist.Name = projectUpdateDto.Name;
            projectexist.Description = projectUpdateDto.Description;
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }
    }
}
