using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.Model;
using DriverLog.View;
using DriverLog.View.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DriverLog.ViewModel.Admin
{
    public partial class EventLogPageViewModel : ObservableObject
    {

        public void LogEvent(string eventType)
        {
            SqlHandler sqlHandler = new();

            EventLogModel elm = new EventLogModel();
            elm.Event_Entry = eventType;
            elm.Date = DateTime.Now;
            elm.UserID = sqlHandler.GetIdFromUsername();

            sqlHandler.AddEventLog(elm);
        }


    }
}
