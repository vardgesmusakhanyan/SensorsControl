using SensorsControl.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories
{
    public class TelemetryRepository : ITelemetryRepository
    {
        public Task AddTelemetryDataAsync(int deviceId, TelemetryEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<DailyTelemetryEntity>> GetAllAsync(int deviceId)
        {
            throw new NotImplementedException();
        }

        public Task<DailyTelemetryEntity> GetAsync(int deviceId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
