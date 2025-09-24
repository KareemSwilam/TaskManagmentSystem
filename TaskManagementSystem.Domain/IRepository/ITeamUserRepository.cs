using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface ITeamUserRepository:IRepository<TeamUser>
    {
        public Task<List<ApplicationUser>> GetUsersByTeam(int teamId);
        public Task<List<Team>> GetTeamsByUser(string userId);
        public Task<List<TeamUser>> GetAllTeamUsers();


    }
}
