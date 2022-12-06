using DriverLog.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class EventLogModel
    {
        public string? Event_Entry { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public LogLevel Loglevel { get; set; }
    }
}
