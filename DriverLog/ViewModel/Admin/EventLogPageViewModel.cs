using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        
        // list of eventlogs
        [ObservableProperty]
        private List<EventLogDTO> eventLogs;


        // constructor
        public EventLogPageViewModel()
        {
            // gets eventlog list from database
            SqlHandler sqlHandler = new();
            EventLogs = sqlHandler.GetEventLogList();
        }
    }
}
