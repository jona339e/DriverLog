using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel
{
    public partial class UserViewModel : ObservableObject
    {


        [RelayCommand]
        public void OnClose()
        {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage("CloseMenuWindow"));
        }
    }
}
