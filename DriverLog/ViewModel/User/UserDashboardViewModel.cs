using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel.User
{
    public partial class UserDashboardViewModel : ObservableObject
    {

        [RelayCommand]
        public void OnNewDriveLog()
        {
            NotifyThisToOpenDriveLog();
        }

        public void NotifyThisToOpenDriveLog()
        {
            WeakReferenceMessenger.Default.Send(new DriveLogWindowControlMessage("OpenWindow"));
        }

    }
}
