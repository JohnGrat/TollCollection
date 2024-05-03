using Business.Helpers;
using Business.Models;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Repositories.Defaults
{
    public class TollRepository : IRepository<TollPassage, TollResult>
    {
        private readonly TollCollectionDbContext _context;

        public TollRepository(TollCollectionDbContext context)
        {
            _context = context;
        }

        public async Task<TollPassage> AddAsync(TollPassage entity)
        {
            _context.TollPassages.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TollResult>> GetAllAsync(DateTime? startDate, DateTime? endDate)
        {
            IQueryable<TollPassage> query = _context.TollPassages;

            if (startDate.HasValue)
            {
                query = query.Where(v => v.Timestamp >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(v => v.Timestamp <= endDate.Value);
            }

            var tollResults = await query
              .Where(v => v.VehicleTypeId == 7)
              .GroupBy(v => new { v.Timestamp.Date, v.Timestamp.Hour })
              .Select(g => new TollResult
              {
                  VehicleRegistrationNumber = g.Key.ToString(),
                  TotalTaxAmount = Math.Min(60, g.Sum(v => TollCalculator.GetTollFee(v.Timestamp, v)))
              })
              .ToListAsync();

            return tollResults;
        }
    }
}
