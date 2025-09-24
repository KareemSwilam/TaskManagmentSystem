using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface IProjectRepository: IRepository<Project>
    {
        Task<ApplicationUser> GetCreatedUser(string name);
        Task<List<Team>> GetProjectteams(string name);
    }
}
