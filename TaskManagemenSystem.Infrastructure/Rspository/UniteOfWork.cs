using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagemenSystem.Infrastructure.Persistence;
using TaskManagementSystem.Domain.IRepository;

namespace TaskManagemenSystem.Infrastructure.Rspository
{
    public class UniteOfWork: IUniteOFWork
    {
        private readonly AppDbContext _context;
        public  ITaskUserRepository _taskUserRepository { get; private set; }
        public  ITAskRepository _tAskRepository { get; private set; }
        public  ITeamUserRepository _teamUserRepositorycs { get; private set; }
        public  ITeamRepository _teamRepository { get; private set; }
        public  IUserRepository _userRepository { get; private set; }
        public  IProjectRepository _projectRepository { get; private set; }

        

        public UniteOfWork(AppDbContext context,
            ITaskUserRepository taskUserRepository,
            ITAskRepository tAskRepository,
            ITeamUserRepository teamUserRepositorycs,
            ITeamRepository teamRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository
            )
        {
            _context = context; 
            _taskUserRepository = taskUserRepository;
            _tAskRepository = tAskRepository;
            _teamUserRepositorycs = teamUserRepositorycs;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public void Dispose()
        { 
           _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();   
        }

        
    }
}
