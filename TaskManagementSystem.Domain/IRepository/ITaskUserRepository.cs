using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface ITaskUserRepository:IRepository<TaskUser>
    {
        public Task<TaskUser> GeTaskUserWithUserAndTask(int id);
        public Task<TaskUser> GeTaskUserWithUser(int id);
        public Task<TaskUser> GeTaskUserWithTask(int id);
        public Task<List<TaskUser>> GetAllTaskUsersWithUserAndTask();
        public Task<List<TaskUser>> GetTaskUsersByUserId(string userId);
        public Task<List<TaskUser>> GetTaskUsersByTaskId(int taskId);
    }
}
