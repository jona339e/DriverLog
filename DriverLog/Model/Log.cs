using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class Log
    {
        public string? Message { get; set; }
        public DateTime Date { get; set; }
        public int Distance { get; set; }
    }
}
