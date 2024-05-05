using Business.Helpers;
using Business.Models;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Business.Repositories.Defaults
{
    public class TollRepository : ITollRepository
    {
        private readonly TollCollectionDbContext _context;

        public TollRepository(TollCollectionDbContext context)
        {
            _context = context;
        }

        public async Task<TollPassage> AddAsync(string registrationNumber, DateTime timestamp, string vehicleTypeName)
        {
            var vehicleType = await _context.VehicleTypes.FirstOrDefaultAsync(vt => vt.TypeName == vehicleTypeName);

            if (vehicleType == null)
            {
                throw new ArgumentException("Invalid vehicle type name.");
            }

            var entity = new TollPassage
            {
                RegistrationNumber = registrationNumber,
                Timestamp = timestamp,
                VehicleTypeId = vehicleType.Id // Set the VehicleTypeId using the Id of the found VehicleType
            };

            _context.TollPassages.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TollResult>> GetAllAsync(DateTime? startDate, DateTime? endDate)
        {
            IQueryable<TollPassage> query = _context.TollPassages;

            if (startDate.HasValue)
                query = query.Where(v => v.Timestamp >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(v => v.Timestamp <= endDate.Value);

            var tollResults = await query
                .Where(v => v.VehicleTypeId == 7)
                .GroupBy(v => new { v.RegistrationNumber, v.Timestamp.Date, v.Timestamp.Hour })
                .Select(g => g.OrderByDescending(v => v.Timestamp.Hour).First())
                .ToListAsync();

            var taxResults = tollResults
                .GroupBy(v => new { v.RegistrationNumber, v.Timestamp.Date })
                .Select(g => new TollResult
                {
                    VehicleRegistrationNumber = g.Key.RegistrationNumber,
                    TotalTaxAmount = Math.Min(60, g.Sum(v => TollCalculator.GetTollFee(v.Timestamp)))
                })
                .GroupBy(v => v.VehicleRegistrationNumber)
                .Select(g => new TollResult
                {
                    VehicleRegistrationNumber = g.Key,
                    TotalTaxAmount = g.Sum(v => v.TotalTaxAmount)
                })
                .Where(v => v.TotalTaxAmount != 0)
                .ToList();

            return taxResults;
        }
    }
}
