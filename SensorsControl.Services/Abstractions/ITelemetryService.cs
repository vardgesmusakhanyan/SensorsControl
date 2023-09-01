using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorsControl.Services.Models;

namespace SensorsControl.Services
{
    public interface ITelemetryService
    {
        Task<List<UnitModel>> GetLastMonthDataAsync(int deviceId);
        Task<bool> AddAsync(int deviceId, List<TelemetryModel> model);
    }
}
