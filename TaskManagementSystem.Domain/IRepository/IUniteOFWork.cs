using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface IUniteOFWork: IDisposable
    {
          ITaskUserRepository _taskUserRepository{get; }
          ITAskRepository _tAskRepository { get; }
        ITeamUserRepository _teamUserRepositorycs { get; }
        ITeamRepository _teamRepository { get; }
        IUserRepository _userRepository { get; }
        IProjectRepository _projectRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
