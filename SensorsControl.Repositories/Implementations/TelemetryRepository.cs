using Microsoft.EntityFrameworkCore;
using SensorsControl.Repositories.Entities;
using SensorsControl.Repositories.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories
{
    public class TelemetryRepository : ITelemetryRepository
    {
        private readonly SensorsControlContext _context;

        public TelemetryRepository(SensorsControlContext context)
        {
            _context = context;
        }

        public async Task<int> UpdateAsync(DailyTelemetryEntity entity)
        {
            _context.DailyTelemetryEntities.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public IQueryable<DailyTelemetryEntity> GetAllAsync(int deviceId)
        {
            return _context.DailyTelemetryEntities.Where(e => e.DeviceId == deviceId).Include(de => de.DailyRecords).AsQueryable();
        }

        public async Task<DailyTelemetryEntity> GetAsync(int deviceId, DateTime date)
        {
            var allEntities = GetAllAsync(deviceId);
            return await allEntities.FirstOrDefaultAsync(e => e.Date == date);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
