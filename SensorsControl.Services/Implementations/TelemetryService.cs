using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SensorsControl.Repositories;
using SensorsControl.Repositories.Entities;
using SensorsControl.Services;
using SensorsControl.Services.Models;

namespace SensorsControl.Services
{
    public class TelemetryService : ITelemetryService
    {
        private readonly ITelemetryRepository _telemetryRepository;
        private readonly IMapper _mapper;

        public TelemetryService(ITelemetryRepository telemetryRepository, IMapper mapper)
        {
            _telemetryRepository = telemetryRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(int deviceId, List<TelemetryModel> models)
        {
            var tEntities = _mapper.Map<List<TelemetryEntity>>(models);
            foreach (var tEntity in tEntities)
            {
                var dEntity = await _telemetryRepository.GetAsync(deviceId, tEntity.Date);
                if (dEntity == null)
                {
                    dEntity = new DailyTelemetryEntity();
                    dEntity.DeviceId = deviceId;
                    dEntity.Date = tEntity.Date;
                    dEntity.DailyRecords = new List<TelemetryEntity>();
                }
                tEntity.DailyEntity = dEntity;
                dEntity.DailyRecords.Add(tEntity);
                await _telemetryRepository.UpdateAsync(dEntity);
            }
            await _telemetryRepository.SaveChangesAsync();
        }

        public async Task<List<UnitModel>> GetLastMonthDataAsync(int deviceId)
        {
            
            var query = _telemetryRepository.GetAllAsync(deviceId);

            query = query.Where(e => e.Date >= (DateTime.Now.Date.AddDays(-30)));

            var list = await query.ToListAsync();

            var models = _mapper.Map<List<UnitModel>>(list);

            return models;
        }
    }
}
