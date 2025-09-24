using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface ITAskRepository: IRepository<TAsk>
    {
        public Task<TAsk> GetTaskWithProject(int id);
        public Task<TAsk> GetTaskWithTeam(int id);
        public Task<TAsk> GetTaskWithCreatedUser(int id);
        public Task<TAsk> GetTaskWithAll(int id);
    }
}
