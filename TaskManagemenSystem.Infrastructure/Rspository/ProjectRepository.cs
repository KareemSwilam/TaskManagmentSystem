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
    public class ProjectRepository:Repository<Project> , IProjectRepository
    {
        
        public ProjectRepository(AppDbContext context) : base(context) { }

        public async Task<ApplicationUser> GetCreatedUser(string name)
        {
            var user = await _dbSet.Where(p => p.Name == name)
                                   .Include(p => p.CreatedUser)
                                   .Select(p => p.CreatedUser)
                                   .FirstOrDefaultAsync();
            return user!;
        }

        public async Task<List<Team>> GetProjectteams(string name)
        {
            var Teams = await  _dbSet.Where(p => p.Name == name)
                                   .Include(p => p.Teams)
                                   .SelectMany(p => p.Teams)
                                   .ToListAsync();

            return Teams;
        }
    }
}
