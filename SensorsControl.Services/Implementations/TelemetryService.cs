using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
            var entities = _mapper.Map<List<TelemetryEntity>>(models);

            foreach(var entity in entities)
            {
                await _telemetryRepository.AddTelemetryDataAsync(deviceId, entity);
            }

            await _telemetryRepository.SaveChangesAsync();
        }

        public async Task<List<UnitModel>> GetLastMonthDataAsync(int deviceId)
        {
            
            var query = await _telemetryRepository.GetAllAsync(deviceId);

            query = query.Where(e => e.Date >= (DateTime.Now.Date.AddDays(-30)));

            var models = _mapper.Map<List<UnitModel>>(query.ToList());

            return models;
        }
    }
}
