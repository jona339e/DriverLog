using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.ViewModel
{
    public partial class DriversLogViewModel : ObservableObject
    {

        public void notifyWindowToClose()
        {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage("CloseWindow"));
        }

    }
}
