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

namespace DriverLog.ViewModel
{
    public partial class DriversLogViewModel : ObservableObject
    {
        [RelayCommand]
        public void OnUser ()
        {
            UserView userView= new UserView ();
            userView.Show();
            NotifyWindowToHide();

        }
        [RelayCommand]
        public void OnVehicle ()
        {
            VehicleView vehicleView= new VehicleView ();
            vehicleView.Show();
            NotifyWindowToHide();

        }
        [RelayCommand]
        public void OnLog ()
        {
            LogView logView= new LogView ();
            logView.Show();
            NotifyWindowToHide();

        }



        public void NotifyWindowToHide()
        {
            WeakReferenceMessenger.Default.Send(new ChangeVisibilityMessage("Hide"));
        }

    }
}
