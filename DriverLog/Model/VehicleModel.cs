using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class VehicleModel
    {
        public string? Model { get; set; }
        public string? Plate { get; set; }
        public string? Status { get; set; } = string.Empty;
    }
}
