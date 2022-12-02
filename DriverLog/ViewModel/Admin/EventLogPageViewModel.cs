using CommunityToolkit.Mvvm.ComponentModel;
using DriverLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel.Admin
{
    public partial class EventLogPageViewModel : ObservableObject
    {
        List<EventLogModel> eventLogs= new List<EventLogModel>();

    }
}
