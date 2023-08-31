using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsControl.Repositories.Entities
{
    public class TelemetryEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DailyTelemetryEntity DailyEntity { get; set; }
        public float Illum { get; set; }
        public DateTime Date { get; set; }
    }
}
