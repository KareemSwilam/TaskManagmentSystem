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
    public interface ITeamServices
    {
        public Task<CustomResult> AddTeam(TeamCreateDto TeamCreateDto);
        public Task<CustomResult<List<TeamDto>>> GetTeams();
        public Task<CustomResult<TeamDto>> GetTeam(string name);
        public Task<CustomResult> DeleteTeam(string name);
        public Task<CustomResult> UpdateTeam(string name, TeamUpdateDto TeamUpdateDto);
        public Task<CustomResult<TeamWithCreatedUserDto>> GetTeamWithGreatedUser(string name);
    }
}
