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
    public class TeamServices : ITeamServices
    {
        private readonly IUniteOFWork _uniteofwork;
        public TeamServices(IUniteOFWork uniteofwork)
        {
            _uniteofwork = uniteofwork;
        }
        public async Task<CustomResult> AddTeam(TeamCreateDto TeamCreateDto)
        {
            if(TeamCreateDto == null)
                return CustomResult.Failure(CustomError.ValidationError("InValid Input"));
            var projectexist = await _uniteofwork._projectRepository.Get(p => p.Id == TeamCreateDto.ProjectId);
            if (projectexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Project Not Found"));
            var userexist = await _uniteofwork._userRepository.UserExist(TeamCreateDto.CreatedUserEmail);
            if (!userexist)
                return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
            var user = await _uniteofwork._userRepository.GetUser(TeamCreateDto.CreatedUserEmail);
            var TeamExist = await _uniteofwork._teamRepository.Get(t => t.Name == TeamCreateDto.Name);
            if (TeamExist != null)
                return CustomResult.Failure(CustomError.ValidationError("Team Exist"));
            var team = new Team
            {
                Name = TeamCreateDto.Name,
                ProjectId = TeamCreateDto.ProjectId,
                CreatedUserId = user.Id,
            };
            await _uniteofwork._teamRepository.Add(team);
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();

        }

        public async Task<CustomResult> DeleteTeam(string name)
        {
            var Teamexist = await _uniteofwork._teamRepository.Get(p => p.Name == name);
            if (Teamexist == null)
                return CustomResult.Failure(CustomError.NotFoundError("Team Not Found "));
            await _uniteofwork._teamRepository.Delete(Teamexist);
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }

        public async Task<CustomResult<TeamDto>> GetTeam(string name)
        {
            var team = await _uniteofwork._teamRepository.Get(t => t.Name == name);
            if (team == null)
                return CustomResult<TeamDto>.Failure(CustomError.NotFoundError("Team Not Found"));
            var teamdto = new TeamDto
            {
                Name = team.Name,
            };
            return CustomResult<TeamDto>.Success(teamdto);
        }

        

        public async  Task<CustomResult<List<TeamDto>>> GetTeams()
        {
            var teams= await _uniteofwork._teamRepository.GetAllAsync();
            if (teams == null)
                return CustomResult<List<TeamDto>>.Failure(CustomError.NotFoundError("Team Not Found"));
            var teamslist = teams.Select(t => new TeamDto { Name = t.Name }).ToList();
            return CustomResult<List<TeamDto>>.Success(teamslist);
        }

       

        public async  Task<CustomResult<TeamWithCreatedUserDto>> GetTeamWithGreatedUser(string name)
        {
            var team = await _uniteofwork._teamRepository.Get(t =>t.Name == name);
            if (team == null)
                return CustomResult<TeamWithCreatedUserDto>.Failure(CustomError.NotFoundError("Team Not Found"));
            var createduser = await _uniteofwork._teamRepository.GetCreatedUser(name);
            var teamwithcreateduserdto = new TeamWithCreatedUserDto
            {
                Name = team.Name,
                CreatedUser = new UserDto
                {
                    FullName = createduser.FullName,
                    Email = createduser.Email!,
                    Role = createduser.Role,
                }
            };
            return CustomResult<TeamWithCreatedUserDto>.Success(teamwithcreateduserdto);
        }

        public async Task<CustomResult> UpdateTeam(string name, TeamUpdateDto TeamUpdateDto)
        {
            var team = await _uniteofwork._teamRepository.Get(t => t.Name == name);
            if (team == null)
                return CustomResult.Failure(CustomError.NotFoundError("Team Not Found"));
            team.Name = TeamUpdateDto.Name;
            team.ProjectId = TeamUpdateDto.ProjectId;
            var complete = await _uniteofwork.SaveChangesAsync();
            if (complete == 0)
                return CustomResult.Failure(CustomError.ServerError("Server Error"));
            return CustomResult.Success();
        }
    }
}
