using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.Model;
using DriverLog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel.User
{
    public partial class AddDriveLogViewModel : ObservableObject
    {

        [RelayCommand]
        public void OnAddDriveCancel()
        {
            NotifyAddDriveWindowToClose();
        }

        [RelayCommand]
        public void OnAddDriveConfirm()
        {
            NotifyAddDriveWindowToClose();
        }

        public void NotifyAddDriveWindowToClose()
        {
            WeakReferenceMessenger.Default.Send(new DriveLogWindowControlMessage("CloseWindow"));
        }


    }
}
