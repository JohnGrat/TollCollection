using Data;
using Data.Models;

namespace Business.Repositories.Defaults
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly TollCollectionDbContext _context;

        public VehicleRepository(TollCollectionDbContext context)
        {
            _context = context;
        }

        public Task<Vehicle> AddAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Vehicle>> GetAllAsync(DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }
    }
}
