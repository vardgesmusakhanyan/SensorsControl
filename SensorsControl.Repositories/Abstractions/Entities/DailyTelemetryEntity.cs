using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories.Entities
{
    public class DailyTelemetryEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TelemetryEntity> DailyRecords { get; set; }
    }
}
