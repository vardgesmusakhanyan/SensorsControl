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
        public Task<int> SaveChangesAsync();
        public Task<int> UpdateAsync(DailyTelemetryEntity entity);
        public Task<DailyTelemetryEntity> GetAsync(int deviceId, DateTime date);
        public IQueryable<DailyTelemetryEntity> GetAllAsync(int deviceId);


    }
}
