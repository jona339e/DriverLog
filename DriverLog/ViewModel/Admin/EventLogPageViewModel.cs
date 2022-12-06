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
        [ObservableProperty]
        private List<EventLogDTO> eventLogs;

        public EventLogPageViewModel()
        {
            SqlHandler sqlHandler = new();
            EventLogs = sqlHandler.GetEventLogList();
        }
    }
}
