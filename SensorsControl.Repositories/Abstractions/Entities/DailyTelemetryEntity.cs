using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories.Entities
{
    public class DailyTelemetryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TelemetryEntity> DailyRecords { get; set; }
    }
}
