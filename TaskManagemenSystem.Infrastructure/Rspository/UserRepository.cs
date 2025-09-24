using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagemenSystem.Infrastructure.Persistence;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagemenSystem.Infrastructure.Rspository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public UserRepository(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task AddUser(string name, string email, string Role)
        {
            var user = new ApplicationUser
            {
                FullName = name,
                Email = email,
                Role = Role,
                UserName = email,

            };
            var roleExist = await _roleManager.RoleExistsAsync(Role);
            if(!roleExist)
                throw new Exception("Role Not Exist");
            var defaultpassword = $"{name}@123";
            var result = await _userManager.CreateAsync(user, defaultpassword);
            await _userManager.AddToRoleAsync(user, Role);
            if (result.Succeeded)
                return;
            throw new Exception("Faild in Add User");
        }

        public async Task ChangePassword(ApplicationUser user, string oldPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
                return;
            throw new Exception("Faild in Change Password");
        }

        public async Task DeleteUser(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return;
            throw new Exception("Faild in Delete User");
        }

        public async Task<List<TAsk>> GetTasksGreateByUser(string Email)
        {
            var Taks = await _userManager.Users.Where(u => u.Email == Email)
                                         .Include(u => u.CreatedTasks)
                                         .Select(u => u.CreatedTasks)
                                         .FirstOrDefaultAsync();
           
            return Taks;            
        }
        public async  Task<bool> UserExist(string Email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if(user != null) return true;
            return false;
        }
        public async Task<ApplicationUser> GetUser(string Email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == Email);
            
                return user!;
            
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<List<TAsk>> GetUserTAsk(string Email)
        {
            var user =await  _userManager.Users.Where(u => u.Email == Email)
                                         .Include(u => u.TaskUsers)
                                         .Select(u => u.TaskUsers)
                                         .AsSplitQuery()
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync();
            
                var tasks = new List<TAsk>();
                foreach (var item in user)
                {
                    var Taks = await _context.Tasks.Where(t => t.Id == item.TaskId).FirstAsync();
                    tasks.Add(Taks);
                } 
                return tasks;
            
        }
        public async Task<List<Team>> GetTeamsGreateByUser(string Email)
        {
            var teams = await _userManager.Users.Where(u => u.Email == Email)
                                         .Include(u => u.CreatedTeams)
                                         .Select(u => u.CreatedTeams)
                                         .FirstOrDefaultAsync();
            
            return teams;
        }
        public async Task<List<Team>> GetUserTeam(string Email)
        {
            var user = await _userManager.Users.Where(u => u.Email == Email)
                                         .Include(u => u.TeamUsers)
                                         .Select(u => u.TeamUsers)
                                         .AsSplitQuery()
                                         .AsNoTracking()
                                         .FirstOrDefaultAsync();
            
            
                var teams = new List<Team>();
                foreach (var item in user)
                {
                    var team = await _context.Teams.Where(t => t.Id == item.TeamId).FirstAsync();
                    teams.Add(team);
                }
                return teams;
           
        }
        public async Task<List<Project>> GetProjectsGreateByUser(string Email)
        {
            var projects = await _userManager.Users.Where(u => u.Email == Email)
                                         .Include(u => u.CreatedProjects)
                                         .Select(u => u.CreatedProjects)
                                         .FirstOrDefaultAsync();
            
            return projects;
        }
    }
}
