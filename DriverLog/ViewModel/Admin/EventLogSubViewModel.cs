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
        // takes data and stores it in database

        public EventLogModel LogEvent(string eventType, LogLevel loglevel, int ID)
        {
            EventLogModel elm = new EventLogModel();
            elm.Event_Entry = eventType;
            elm.Date = DateTime.Now;
            elm.UserID = ID; 
            elm.Loglevel = loglevel;
            return elm;
        }        

        // overload of above method
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
    public enum LogLevel // enum that shows the level of eventLog
    {
        Information,
        Warning,
        Error,
        Fatal
    }

}
