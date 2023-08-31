using Microsoft.EntityFrameworkCore;
using SensorsControl.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories.EntityFrameworkCore
{
    public class SensorsControlContext : DbContext
    {
        public SensorsControlContext(DbContextOptions<SensorsControlContext> options) :base(options)
        {
        }

        public DbSet<DailyTelemetryEntity> DailyTelemetryEntities { get; set; }
        public DbSet<TelemetryEntity> TelemetryEntities { get; set; }
    }
}
