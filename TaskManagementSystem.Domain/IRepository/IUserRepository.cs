using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface IUserRepository
    {
        public Task AddUser(string name, string email, string Role);
        public Task<ApplicationUser> GetUser(string Email);
        public Task<List<ApplicationUser>> GetUsers();
        public Task DeleteUser(ApplicationUser user);
        public Task ChangePassword(ApplicationUser user ,  string oldPassword, string newPassword);
        public Task<List<TAsk>> GetUserTAsk(string Email );
        public Task<List<TAsk>> GetTasksGreateByUser(string Email);
        public Task<List<Team>> GetUserTeam(string Email);
        public Task<List<Team>> GetTeamsGreateByUser(string Email);
        public Task<List<Project>> GetProjectsGreateByUser(string Email);
        public Task<bool> UserExist(string Email);
    }
}
