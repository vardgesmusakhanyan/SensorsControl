using System.Text.Json.Serialization;

namespace SensorsControl.WebAPI.Dto
{
    public class UnitDto
    {
        public string Date { get; set; }
        public float MaxIlluminance { get; set; }
    }
}
