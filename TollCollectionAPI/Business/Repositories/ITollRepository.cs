using Business.Models;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public interface ITollRepository
    {
        Task<IEnumerable<TollResult>> GetAllAsync(DateTime? startDate, DateTime? endDate);

        Task<TollPassage> AddAsync(string registrationNumber, DateTime timestamp, string vehicleTypeName);
    }
}
