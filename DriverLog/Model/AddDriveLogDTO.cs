using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class DriveLogDTO
    {
        public string Username { get; set; }
        public string Plate { get; set; }
        public DateTime Date { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Distance { get; set; }

    }
}
