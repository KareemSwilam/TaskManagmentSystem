using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter , bool tarcking = true);
        Task Add( T Entity);    
        Task Update(T Entity);
        Task Delete(T Entity);
    }
    
}
