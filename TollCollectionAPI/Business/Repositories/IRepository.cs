using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface IRepository<T, TU>
    {
        Task<IEnumerable<TU>> GetAllAsync(DateTime? startDate, DateTime? endDate);

        Task<T> AddAsync(T entity);
    }
}
