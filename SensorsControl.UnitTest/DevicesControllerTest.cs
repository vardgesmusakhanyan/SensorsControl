using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SensorsControl.Services;
using SensorsControl.Services.Models;
using SensorsControl.WebAPI.Controllers;
using SensorsControl.WebAPI.Dto;

namespace SensorsControl.UnitTests
{
    public class DevicesControllerTest
    {
        [Fact]
        public async Task Telemetry_ValidData_ReturnsOk()
        {
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var mapperMock = new Mock<IMapper>();

            var controller = new DevicesController(telemetryServiceMock.Object, mapperMock.Object);

            var deviceId = 1;
            var telemetryDtos = new List<TelemetryDto> {
                new TelemetryDto { Illum = 123.5f, Time = 1692946687 },
                new TelemetryDto { Illum = 123.0f, Time = 1692947687 },
                new TelemetryDto { Illum = 122.5f, Time = 1692948687 },
                new TelemetryDto { Illum = 122.0f, Time = 1692949687 }
             };

            telemetryServiceMock
                .Setup(service => service.AddAsync(deviceId, It.IsAny<List<TelemetryModel>>()))
                .ReturnsAsync(true);

            var okResult = await controller.Telemetry(deviceId, telemetryDtos) as OkResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            telemetryServiceMock
                .Setup(service => service.AddAsync(deviceId, It.IsAny<List<TelemetryModel>>()))
                .ReturnsAsync(false);

            var badResult = await controller.Telemetry(deviceId, telemetryDtos) as BadRequestResult;

            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact]
        public async Task Statistics_GetLastMonthData_ReturnsOk()
        {
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var mapperMock = new Mock<IMapper>();

            var controller = new DevicesController(telemetryServiceMock.Object, mapperMock.Object);

            var deviceId = 1;

            var unitDtos = new List<UnitDto> {
                new UnitDto { MaxIlluminance = 123.5f, Date = DateTime.Now.Date.ToString() },
                new UnitDto { MaxIlluminance = 123.0f, Date = DateTime.Now.Date.AddDays(1).ToString() },
                new UnitDto { MaxIlluminance = 122.5f, Date = DateTime.Now.Date.AddDays(2).ToString() },
                new UnitDto { MaxIlluminance = 122.0f, Date = DateTime.Now.Date.AddDays(3).ToString() }
             };


            var unitModels = new List<UnitModel> {
                new UnitModel { MaxIlluminance = 123.5f, Date = DateTime.Now.Date },
                new UnitModel { MaxIlluminance = 123.0f, Date = DateTime.Now.Date.AddDays(1) },
                new UnitModel { MaxIlluminance = 122.5f, Date = DateTime.Now.Date.AddDays(2) },
                new UnitModel { MaxIlluminance = 122.0f, Date = DateTime.Now.Date.AddDays(3) }
             };

            telemetryServiceMock
                .Setup(service => service.GetLastMonthDataAsync(deviceId))
                .ReturnsAsync(unitModels);

            mapperMock.Setup(mapper => mapper.Map<List<UnitDto>>(unitModels))
                .Returns(unitDtos);

            var okResult = await controller.Statistics(deviceId) as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            var returnedData = okResult.Value as List<UnitDto>;
            Assert.NotNull(returnedData);
            Assert.Equal(unitDtos, returnedData, new UnitDtoComparer());

        }


        [Fact]
        public async Task Statistics_NoData_ReturnsNotFound()
        {
            var telemetryServiceMock = new Mock<ITelemetryService>();
            var mapperMock = new Mock<IMapper>();

            var controller = new DevicesController(telemetryServiceMock.Object, mapperMock.Object);

            var deviceId = 1;

            telemetryServiceMock
                .Setup(service => service.GetLastMonthDataAsync(deviceId))
                .Returns(Task.FromResult<List<UnitModel>>(null));

            var result = await controller.Statistics(deviceId) as NotFoundResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }

    public class UnitDtoComparer : IEqualityComparer<UnitDto>
    {
        public bool Equals(UnitDto x, UnitDto y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }

            return x.Date == y.Date &&
                   x.MaxIlluminance == y.MaxIlluminance;

        }

        public int GetHashCode(UnitDto obj)
        {
            int hashCode = 31;
            hashCode = hashCode * 23 + obj.Date.GetHashCode();
            hashCode = hashCode * 23 + obj.MaxIlluminance.GetHashCode();

            return hashCode;
        }
    }
}