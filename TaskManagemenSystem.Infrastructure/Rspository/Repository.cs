using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagemenSystem.Infrastructure.Persistence;
using TaskManagementSystem.Domain.IRepository;

namespace TaskManagemenSystem.Infrastructure.Rspository
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        protected  readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T Entity)
        {
            await _dbSet.AddAsync(Entity);  
        }

        public  Task Delete(T Entity)
        {
             _dbSet.Remove(Entity);
            return Task.CompletedTask;
        }

        public Task<T> Get(Expression<Func<T, bool>> filter, bool tarcking = true)
        {
           IQueryable<T> query = _dbSet;
            if (!tarcking)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefaultAsync(filter)!;   
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public  Task Update(T Entity)
        {
            _dbSet.Update(Entity);
            return Task.CompletedTask;
        }
    }
}
