using SensorsControl.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories
{
    public interface ITelemetryRepository
    {
        public Task SaveChangesAsync();
        public Task AddTelemetryDataAsync(int deviceId, TelemetryEntity entity);
        public Task<DailyTelemetryEntity> GetAsync(int deviceId, DateTime date);
        public Task<IQueryable<DailyTelemetryEntity>> GetAllAsync(int deviceId);


    }
}
