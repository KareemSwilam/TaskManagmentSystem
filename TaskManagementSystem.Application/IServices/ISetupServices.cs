using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Application.Result;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Application.IServices
{
    public interface ISetupServices
    {
        public Task<CustomResult> AddRole(string name);
        public Task<CustomResult> RemoveRole(string name);
        public Task<CustomResult> AddRoleToUser(string  userEmail, string roleName);
        public Task<CustomResult<List<string>>> GetRoles();
        public Task<CustomResult<IList<string>>> GetUserRoles(string userEmail);
        public Task<CustomResult<bool>> CheckUserInRole(string userEmail, string role);
        public Task<CustomResult> RemoveRoleFromUser(string userEmail, string roleName);
    }
}
