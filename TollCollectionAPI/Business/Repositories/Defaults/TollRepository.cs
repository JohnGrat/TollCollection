﻿using Business.Helpers;
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
                VehicleTypeId = vehicleType.Id
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

            var tollRecords = await query
                .Where(v => v.VehicleTypeId == 7)
                .GroupBy(v => new { v.RegistrationNumber, v.Timestamp.Date })
                .Select(g => g.Select(v => v).ToList())
                .ToListAsync();

            List<TollResult> tollResults = new List<TollResult>();
            foreach (var tollRecord in tollRecords)
            {
                var taxResults = TollCalculator.CalculateTaxPerDay(tollRecord);
                tollResults.Add(taxResults);
            }

            var aggregatedResults = tollResults
                .GroupBy(t => t.VehicleRegistrationNumber)
                .Select(group => new TollResult
                {
                    VehicleRegistrationNumber = group.Key,
                    TotalTaxAmount = group.Sum(t => t.TotalTaxAmount)
                })
                .Where(t => t.TotalTaxAmount != 0)
                .ToList();

            return aggregatedResults;
        }
    }
}
