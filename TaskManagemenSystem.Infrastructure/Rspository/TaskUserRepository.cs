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
    public class TaskUserRepository:Repository<TaskUser> ,ITaskUserRepository
    {
        public TaskUserRepository(AppDbContext context):base(context)
        {

        }

        public async Task<TaskUser> GeTaskUserWithTask(int id)
        {
            var result = await _dbSet.Where(t => t.Id == id)
                                     .Include(t => t.Task)
                                     .FirstOrDefaultAsync();
            return result!;

        }

        public async Task<TaskUser> GeTaskUserWithUser(int id)
        {
            var result = await _dbSet.Where(t => t.Id == id)
                                     .Include(t => t.User)
                                     .FirstOrDefaultAsync();
            return result!;
        }

        public async Task<TaskUser> GeTaskUserWithUserAndTask(int id)
        {
            var result = await _dbSet.Where(t => t.Id == id)
                                     .Include(t => t.Task)
                                     .Include(t => t.Task)
                                     .FirstOrDefaultAsync();
            return result!;
        }

        public async Task<List<TaskUser>> GetTaskUsersByTaskId(int taskId)
        {
            var result = await _dbSet.Where(t => t.TaskId == taskId)
                                     .Include(t => t.User)
                                     .ToListAsync();
            return result!;
        }

        public async Task<List<TaskUser>> GetTaskUsersByUserId(string userId)
        {
            var result = await _dbSet.Where(t => t.UserId == userId)
                                     .Include(t => t.Task)
                                     .ToListAsync();
            return result!;
        }

        public async Task<List<TaskUser>> GetAllTaskUsersWithUserAndTask()
        {
            var result = await _dbSet.Include(t => t.Task)
                                     .Include(t => t.Task)
                                     .ToListAsync();
            return result!;
        }
    }
}
