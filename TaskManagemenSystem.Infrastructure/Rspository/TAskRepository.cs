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
    public class TAskRepository:Repository<TAsk> ,ITAskRepository
    {
        public TAskRepository(AppDbContext context) : base(context) { }

        public async Task<TAsk> GetTaskWithAll(int id)
        {
            var Task = await _dbSet.Where(t => t.Id == id)
                                .Include(t => t.Team)
                                .Include(t => t.Project)
                                .Include(t => t.CreatedUser)
                                .FirstOrDefaultAsync();
            return Task!;
        }

        public async Task<TAsk> GetTaskWithCreatedUser(int id)
        {
            var Task = await _dbSet.Where(t => t.Id == id)
                                .Include(t => t.CreatedUser)
                                .FirstOrDefaultAsync();
            return Task!;
        }

        public async Task<TAsk> GetTaskWithProject(int id)
        {
            var Task = await _dbSet.Where(t => t.Id == id)
                                .Include(t => t.Project)
                                .FirstOrDefaultAsync();
            return Task!;
        }

        public async Task<TAsk> GetTaskWithTeam(int id)
        {
            var Task = await _dbSet.Where(t => t.Id == id)
                                .Include(t => t.Team)
                                .FirstOrDefaultAsync(); 
            return Task!;
        }
    }
}
