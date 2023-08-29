using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories.Entities
{
    public class TelemetryEntity
    {
        public int Id { get; set; }
        public DailyTelemetryEntity DailyEntity { get; set; }
        public float Illum { get; set; }
        public DateTime Time { get; set; }
    }
}
