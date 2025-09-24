using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.TeamUserDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;

namespace TaskManagementSystem.Application.IServices
{
    public interface ITeamUserServices
    {
        public Task<CustomResult> AddUserToTeam(TeamUserCreateDto createDto);
        public Task<CustomResult> RemoveUserFromTeam(TeamUserCreateDto createDto);
        public Task<CustomResult> UpdateUserRoleinTeam(TeamUserCreateDto createDto);
        public Task<CustomResult<List<UserDto>>> GetUsersByTeam(int teamId);
        public Task<CustomResult<List<TeamDto>>> GetTeamsByUser(string userEmail);
        public Task<CustomResult<List<TeamUserDto>>> GetAllTeamUsers();
        public Task<CustomResult<int>> CountUsersInTeam(int teamId);
        public Task<CustomResult<int>> CountTeamsForUser(string Email);
    }
}
