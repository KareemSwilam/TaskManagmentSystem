using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.TeamUserDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.Services
{
    public class TeamUserServices : ITeamUserServices
    {
        private readonly IUniteOFWork _uniteofwork;
        public TeamUserServices(IUniteOFWork uniteofwork)
        {
            _uniteofwork = uniteofwork;
        }
        public async Task<CustomResult> AddUserToTeam(TeamUserCreateDto createDto)
        {
            var userexist = await _uniteofwork._userRepository.UserExist(createDto.UserEmail);
            if(!userexist) return CustomResult.Failure(CustomError.NotFoundError("User not found"));
            var teamexist = await _uniteofwork._teamRepository.Get(t => t.Id == createDto.TeamId);
            if (teamexist == null) return CustomResult.Failure(CustomError.NotFoundError("Team not found"));
            var user  = await _uniteofwork._userRepository.GetUser( createDto.UserEmail);
            var teamuserExist = await _uniteofwork._teamUserRepositorycs.Get(tu => tu.TeamId == createDto.TeamId && tu.UserId == user.Id);
            if (teamuserExist != null) return CustomResult.Failure(CustomError.ValidationError("User already in team"));
            var teamuser = new TeamUser
            {
                TeamId = createDto.TeamId,
                UserId = user.Id,
                Role = user.Role,
                JoinedAt = DateTime.UtcNow
            };
            await _uniteofwork._teamUserRepositorycs.Add(teamuser);
            var complete = await _uniteofwork.SaveChangesAsync(); 
            if (complete == 0) return CustomResult.Failure(CustomError.ServerError("Failed to add user to team"));
            return CustomResult.Success();
        }

        public async Task<CustomResult<int>> CountTeamsForUser(string Email)
        {
            var userexist = await _uniteofwork._userRepository.UserExist(Email);
            if (!userexist) return CustomResult<int>.Failure(CustomError.NotFoundError("User not found"));
            var user = await _uniteofwork._userRepository.GetUser(Email);
            var teams = await _uniteofwork._teamUserRepositorycs.GetTeamsByUser(user.Id);
            var count = teams.Count();
            return CustomResult<int>.Success(count);
        }

        public async  Task<CustomResult<int>> CountUsersInTeam(int teamId)
        {
            var teamexist = await _uniteofwork._teamRepository.Get(t => t.Id == teamId);
            if (teamexist == null) return CustomResult<int>.Failure(CustomError.NotFoundError("Team not found"));
            var users = await _uniteofwork._teamUserRepositorycs.GetUsersByTeam(teamId);
            var count = users.Count();
            return CustomResult<int>.Success(count);
        }

        public async Task<CustomResult<List<TeamUserDto>>> GetAllTeamUsers()
        {
            var result = await  _uniteofwork._teamUserRepositorycs.GetAllTeamUsers();
            var teamuserdto = result.Select(tu => new TeamUserDto
            {
                User = new UserDto
                {
                    FullName = tu.User.FullName,
                    Email = tu.User.Email!,
                    Role = tu.User.Role
                },
                Team = new TeamDto
                {
                    Name = tu.Team.Name
                },
                JoinedAt = tu.JoinedAt
            }).ToList();
            return CustomResult<List<TeamUserDto>>.Success(teamuserdto);
        }

        public async Task<CustomResult<List<TeamDto>>> GetTeamsByUser(string userEmail)
        {
            var userexist = await  _uniteofwork._userRepository.UserExist(userEmail);
            if (!userexist) return CustomResult<List<TeamDto>>.Failure(CustomError.NotFoundError("User not found"));
            var user = await _uniteofwork._userRepository.GetUser(userEmail);
            var teams = await _uniteofwork._teamUserRepositorycs.GetTeamsByUser(user.Id);
            var teamDtos = teams.Select(t => new TeamDto { Name = t.Name }).ToList();
            return CustomResult<List<TeamDto>>.Success(teamDtos);
        }

        public async Task<CustomResult<List<UserDto>>> GetUsersByTeam(int teamId)
        {
            var teamexist =  await _uniteofwork._teamRepository.Get(t => t.Id == teamId);
            if(teamexist == null) return CustomResult<List<UserDto>>.Failure(CustomError.NotFoundError("Team not found"));
            var users = await _uniteofwork._teamUserRepositorycs.GetUsersByTeam(teamId);
            var userDtos = users.Select(u => new UserDto { FullName = u.FullName,Email = u.Email!, Role = u.Role }).ToList();
            return CustomResult<List<UserDto>>.Success(userDtos);
        }

        public async Task<CustomResult> RemoveUserFromTeam(TeamUserCreateDto createDto)
        {
            var userexist = await _uniteofwork._userRepository.UserExist(createDto.UserEmail);
            if (!userexist) return CustomResult.Failure(CustomError.NotFoundError("User not found"));
            var teamexist = await _uniteofwork._teamRepository.Get(t => t.Id == createDto.TeamId);
            if (teamexist == null) return CustomResult.Failure(CustomError.NotFoundError("Team not found"));
            var user = await _uniteofwork._userRepository.GetUser(createDto.UserEmail);
            var teamuserExist = await _uniteofwork._teamUserRepositorycs.Get(tu => tu.TeamId == createDto.TeamId && tu.UserId == user.Id);
            if (teamuserExist == null) return CustomResult.Failure(CustomError.ValidationError("User not member in this team team"));
            
            await _uniteofwork._teamUserRepositorycs.Delete(teamuserExist);
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0) return CustomResult.Failure(CustomError.ServerError("Failed to add user to team"));
            return CustomResult.Success();
        }

        public async  Task<CustomResult> UpdateUserRoleinTeam(TeamUserCreateDto createDto)
        {
            var userexist = await _uniteofwork._userRepository.UserExist(createDto.UserEmail);
            if (!userexist) return CustomResult.Failure(CustomError.NotFoundError("User not found"));
            var teamexist = await _uniteofwork._teamRepository.Get(t => t.Id == createDto.TeamId);
            if (teamexist == null) return CustomResult.Failure(CustomError.NotFoundError("Team not found"));
            var user = await _uniteofwork._userRepository.GetUser(createDto.UserEmail);
            var teamuserExist = await _uniteofwork._teamUserRepositorycs.Get(tu => tu.TeamId == createDto.TeamId && tu.UserId == user.Id);
            if (teamuserExist == null) return CustomResult.Failure(CustomError.ValidationError("User not member in this team team"));

            teamuserExist.Role = user.Role; 
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0) return CustomResult.Failure(CustomError.ServerError("Failed to add user to team"));
            return CustomResult.Success();
        }
    }
}
