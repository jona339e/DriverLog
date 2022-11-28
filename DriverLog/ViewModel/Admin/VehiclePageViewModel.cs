using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DriverLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DriverLog.ViewModel.Admin
{
    public partial class VehiclePageViewModel : ObservableObject
    {
        [ObservableProperty]
        public string? createModel;

        [ObservableProperty]
        public string? createPlate;  

        [ObservableProperty]
        public string? createStatus;

        [RelayCommand]
        public void OnCreateVehicle()
        {
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.Model = createModel;
            vehicleModel.Plate = createPlate;
            CreateModel = string.Empty;
            CreatePlate = string.Empty;

            MessageBox.Show($"Oprettede {vehicleModel.Model} med Nr. Plade: {vehicleModel.Plate}");

        }

        [RelayCommand]
        public void OnCreateStatus()
        {
            // TODO - Få liste med BIL ID fra SQL
            //      - Opret status
            //      - Send status tilbage til tilhørende BilID
        }

    }
}
