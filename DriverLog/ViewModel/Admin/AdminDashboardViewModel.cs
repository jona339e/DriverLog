using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DriverLog.ViewModel
{
    public partial class AdminDashboardViewModel : ObservableObject
    {

        [RelayCommand]
        public void OnHome()
        {
            WeakReferenceMessenger.Default.Send(new PageMessage("Home"));

        }

        [RelayCommand]
        public void OnUser()
        {
            WeakReferenceMessenger.Default.Send(new PageMessage("User"));

        }
        [RelayCommand]
        public void OnAbsence()
        {
            WeakReferenceMessenger.Default.Send(new PageMessage("Absence"));

        }

        [RelayCommand]
        public void OnVehicle()
        {
            WeakReferenceMessenger.Default.Send(new PageMessage("Vehicle"));

        }

        [RelayCommand]
        public void OnDriversLog()
        {
            WeakReferenceMessenger.Default.Send(new PageMessage("DriversLog"));

        }

        [RelayCommand]
        public void OnEventLog()
        {
            WeakReferenceMessenger.Default.Send(new PageMessage("EventLog"));

        }


    }
}
