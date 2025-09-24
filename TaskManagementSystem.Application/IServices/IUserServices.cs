using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Application.Dtos.ProjectDtos;
using TaskManagementSystem.Application.Dtos.TeamDtos;
using TaskManagementSystem.Application.Dtos.UserDtos;

namespace TaskManagementSystem.Application.IServices
{
    public interface IUserServices
    {
        public Task<CustomResult<UserDto>> GetUser(string Email);
        public Task<CustomResult<List<UserDto>>> GetUsers();
        public Task<CustomResult<List<ProjectDto>>> GetProjectGreatedByUser(string Email);
        public Task<CustomResult<List<TeamDto>>> GetTeamGreatedByUser(string Email);
        public Task<CustomResult> AddUser(UserCreateDto userCreateDto);
        public Task<CustomResult> ChangePassword(ChangePasswordRequest request);
        public Task<CustomResult> DeleteUser(string Email);  
    }
}
