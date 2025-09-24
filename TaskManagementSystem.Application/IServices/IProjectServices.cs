using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;

namespace TaskManagementSystem.Application.IServices
{
    public interface IProjectServices
    {
        public Task<CustomResult> AddProject(ProjectCreateDto projectCreateDto);
        public Task<CustomResult<List<ProjectDto>>> GetProjects();
        public Task<CustomResult<ProjectDto>> GetProject(string name);

        public Task<CustomResult<List<TeamDto>>> GetProjectTeams(string name);
        public Task<CustomResult> DeleteProject(string name);
        public Task<CustomResult> UpdateProject(string name, ProjectUpdateDto projectUpdateDto);
        public Task<CustomResult<ProjectWithCreatedUserDto>> GetProjectsWithGreatedUser(string name);
    }
}
