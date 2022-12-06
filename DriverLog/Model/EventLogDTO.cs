using DriverLog.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class EventLogDTO
    {
        public string? Event_Entry { get; set; }
        public LogLevel Loglevel { get; set; }
        public string? Username { get; set; }
        public DateTime Date { get; set; }
    }
}
