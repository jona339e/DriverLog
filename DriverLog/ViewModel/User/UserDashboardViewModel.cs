using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel.User
{
    public partial class UserDashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        List<DriveLogDTO> listDriveLog;

        SqlHandler sqlhandler = new();

        // Sends message to open a window on button press
        [RelayCommand]
        public void OnNewDriveLog()
        {
            WeakReferenceMessenger.Default.Send(new DriveLogWindowControlMessage("OpenWindow"));
        }

        [RelayCommand]
        public void OnLogOut()
        {
            MainWindow mw = new();
            mw.Show();
            WeakReferenceMessenger.Default.Send(new UserWindowControlMessage("CloseWindow"));
           
        }

        public UserDashboardViewModel()
        {
            listDriveLog = sqlhandler.GetDriveLog(GlobalUsername.Username, true);
        }

    }
}
