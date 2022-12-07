using DriverLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel.Admin
{
    public partial class EventLogSubViewModel: EventLogPageViewModel
    {
        public EventLogModel LogEvent(string eventType, LogLevel loglevel, int ID)
        {
            EventLogModel elm = new EventLogModel();
            elm.Event_Entry = eventType;
            elm.Date = DateTime.Now;
            elm.UserID = ID; 
            elm.Loglevel = loglevel;
            return elm;
        }        
        public void LogEvent(string eventType, LogLevel loglevel)
        {
            SqlHandler sqlHandler= new SqlHandler();
            EventLogModel elm = new EventLogModel();
            elm.Event_Entry = eventType;
            elm.Date = DateTime.Now;
            elm.UserID = sqlHandler.GetIdFromUsername(); 
            elm.Loglevel = loglevel;
            sqlHandler.AddEventLog(elm);
        }
    }
    public enum LogLevel
    {
        Information,
        Warning,
        Error,
        Fatal
    }

}
