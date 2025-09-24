using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagemenSystem.Infrastructure.Persistence;
using TaskManagementSystem.Domain.IRepository;
using TaskManagementSystem.Domain.Models;

namespace TaskManagemenSystem.Infrastructure.Rspository
{
    public class TeamUserRepository:Repository<TeamUser> , ITeamUserRepository
    {
        public TeamUserRepository(AppDbContext context) : base(context) { }

        public Task<List<TeamUser>> GetAllTeamUsers()
        {
            var teamUsers = _dbSet.Include(tu => tu.Team)
                                  .Include(tu => tu.User)
                                  .ToListAsync();
            return teamUsers;
        }

        public async Task<List<Team>> GetTeamsByUser(string userId)
        {
            var teams = await _dbSet.Where(tu => tu.UserId == userId)
                              .Include(t => t.Team)
                              .Select(tu => tu.Team).ToListAsync();
            return teams;
        }

        public async Task<List<ApplicationUser>> GetUsersByTeam(int teamId)
        {
            var users = await _dbSet.Where(tu => tu.TeamId == teamId)
                              .Include(t => t.User)
                              .Select(tu => tu.User).ToListAsync();
            return users;
        }
    }
}
