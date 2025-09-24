using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface ITeamRepository:IRepository<Team>
    {
        Task<ApplicationUser> GetCreatedUser(string name);
    }
}
