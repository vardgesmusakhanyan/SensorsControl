using Microsoft.AspNetCore.Mvc;
using SensorsControl.WebAPI.Dto;
using SensorsControl.Services;
using AutoMapper;
using SensorsControl.Services.Models;

namespace SensorsControl.WebAPI.Controllers
{
    [ApiController]
    [Route("devices")]
    public class DevicesController : ControllerBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IMapper _mapper;

        public DevicesController(ITelemetryService telemetryService, IMapper mapper)
        {
            _telemetryService = telemetryService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("{deviceId:int}/telemetry")]
        public async Task<IActionResult> Telemetry(int deviceId, [FromBody] List<TelemetryDto> data)
        {
            var model = _mapper.Map<List<TelemetryModel>>(data);
            await _telemetryService.AddAsync(deviceId, model);

            return Ok();
        }

        [HttpGet]
        [Route("{deviceId:int}/statistics")]
        public async Task<IActionResult> Statistics(int deviceId)
        {
            var stat = await _telemetryService.GetLastMonthDataAsync(deviceId);

            if (stat == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<UnitDto>>(stat));
        }
    }
}