using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;
using TaskManagementSystem.Application.IServices;
using TaskManagementSystem.Domain.IRepository;

namespace TaskManagementSystem.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUniteOFWork _uniteOFWork;
        public UserServices(IUniteOFWork uniteOFWork)
        {
            _uniteOFWork = uniteOFWork;
        }

        public async Task<CustomResult> AddUser(UserCreateDto userCreateDto)
        {
            try
            {
                if(userCreateDto == null)
                    return CustomResult.Failure(CustomError.ValidationError("Invalid Input"));
                var user = await _uniteOFWork._userRepository.UserExist(userCreateDto.Email);
                if(user)
                    return CustomResult.Failure(CustomError.ValidationError("User Already Exists"));
                await _uniteOFWork._userRepository.AddUser(userCreateDto.Name, userCreateDto.Email, userCreateDto.Role);
               
                return CustomResult.Success();

            }
            catch(Exception ex)
            {
                return CustomResult.Failure(CustomError.ValidationError(ex.Message));
            }
        }

        public async Task<CustomResult> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var userExistt  = await _uniteOFWork._userRepository.UserExist(request.Email);
                if (!userExistt)
                    return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
                var user = await _uniteOFWork._userRepository.GetUser(request.Email);
                await _uniteOFWork._userRepository.ChangePassword(user, request.OldPassword, request.NewPassword);
                return CustomResult.Success();
            }
            catch(Exception ex)
            {
                return CustomResult.Failure(CustomError.ValidationError(ex.Message));
            }

        }

        public async Task<CustomResult<UserDto>> GetUser(string Email)
        {
           if(Email == null) return  CustomResult<UserDto>.Failure(CustomError.ValidationError("Invalid Input"));
           try 
           {
                var user = await _uniteOFWork._userRepository.GetUser(Email);
                var userdto = new UserDto
                {                    
                    FullName = user.FullName,
                    Email = user.Email!,
                    Role = user.Role,                    
                };
                return CustomResult<UserDto>.Success(userdto);
            }
           catch(Exception ex)
           {
                return CustomResult<UserDto>.Failure(CustomError.NotFoundError(ex.Message));
           }
           
        }

        public async Task<CustomResult<List<UserDto>>> GetUsers()
        {
            var users = await _uniteOFWork._userRepository.GetUsers();
            var usersdto = users.Select(user => new UserDto
            {
                FullName = user.FullName,
                Email = user.Email!,
                Role = user.Role,
            }).ToList();
            return CustomResult<List<UserDto>>.Success(usersdto);
        }

        public async Task<CustomResult> DeleteUser(string Email)
        {
            try
            {
                var userexist = await  _uniteOFWork._userRepository.UserExist(Email);
                if (!userexist) return CustomResult.Failure(CustomError.NotFoundError("User Not Found"));
                var user = await _uniteOFWork._userRepository.GetUser(Email);
                await _uniteOFWork._userRepository.DeleteUser(user);
                return CustomResult.Success();
            }
            catch(Exception ex)
            {
                return CustomResult.Failure(CustomError.ServerError(ex.Message));
            }
            
        }
        public async Task<CustomResult<List<ProjectDto>>> GetProjectGreatedByUser(string Email)
        {
            var userexist = await _uniteOFWork._userRepository.UserExist(Email);
            if (!userexist) return CustomResult<List<ProjectDto>>.Failure(CustomError.NotFoundError("User Not Found"));
            var projects = await _uniteOFWork._userRepository.GetProjectsGreateByUser(Email);
            var projectdtos = projects.Select(project => new ProjectDto
            {
                Name = project.Name,
                Description = project.Description,
            }).ToList();
            return CustomResult<List<ProjectDto>>.Success(projectdtos);
        }
        public async Task<CustomResult<List<TeamDto>>> GetTeamGreatedByUser(string Email)
        {
            var userexist = await _uniteOFWork._userRepository.UserExist(Email);
            if (!userexist) return CustomResult<List<TeamDto>>.Failure(CustomError.NotFoundError("User Not Found"));
            var teams = await _uniteOFWork._userRepository.GetTeamsGreateByUser(Email);
            var teamdtos = teams.Select(project => new TeamDto
            {
                Name = project.Name,
                
            }).ToList();
            return CustomResult<List<TeamDto>>.Success(teamdtos);
        }

    }
}
