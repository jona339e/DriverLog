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

        public SqlHandler sqlHandler = new();
      
        
        public List<int> vehicleList { get; set; }
      

        // Create Vehicle Field

        [ObservableProperty]
        public string? createModel;

        [ObservableProperty]
        public string? createPlate;




        // Update Vehicle Field

        [ObservableProperty]
        public string? updateModel;

        [ObservableProperty]
        public string? updatePlate;

        [ObservableProperty]
        public List<int> updateVehicleID;        
        
        [ObservableProperty]
        public int? updateSelectedVehicleID;



        // Delete Vehicle Field

        [ObservableProperty]
        public List<int> deleteVehicleID;

        [ObservableProperty]
        public int? deleteSelectedVehicleID;

        [ObservableProperty]
        public string? deleteModel;

        [ObservableProperty]
        public string? deletePlate;


        // Change Status Field

        [ObservableProperty]
        public List<int> statusVehicleID;

        [ObservableProperty]
        public int? statusSelectedVehicleID;

        [ObservableProperty]
        public string? changeStatus;



        // Constructor
        public VehiclePageViewModel()
        {
            vehicleList = sqlHandler.GetUserIDList("ID_VEHICLE", "VEHICLE");
            UpdateIDList();
        }


        // Bound Methods

        [RelayCommand]
        public void OnCreateVehicle()
        {
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.Model = CreateModel;
            vehicleModel.Plate = CreatePlate;

            SqlHandler sqlh = new SqlHandler();
            sqlh.CreateVehicle(vehicleModel);

            createModel = string.Empty;
            createPlate = string.Empty;

            MessageBox.Show($"Oprettede {vehicleModel.Model} med Nr. Plade: {vehicleModel.Plate}");

        }

        [RelayCommand]
        public void OnUpdateVehicle() 
        {
            VehicleModel vehicleModel = new();
            vehicleModel.Model = UpdateModel;
            vehicleModel.Plate = UpdatePlate;

            sqlHandler.UpdateVehicle(vehicleModel, updateSelectedVehicleID);


            UpdateIDList();
        }

        [RelayCommand]
        public void OnShowVehicleData()
        {
            VehicleModel vehicleModel = new();
            vehicleModel = sqlHandler.GetVehicleData(DeleteSelectedVehicleID);

            DeleteModel = vehicleModel.Model;
            DeletePlate = vehicleModel.Plate;

        }

        [RelayCommand]
        public void OnChangeStatus()
        {
            VehicleModel vehicleModel = new();
            vehicleModel.Status = ChangeStatus;

            sqlHandler.UpdateStatus(vehicleModel, statusSelectedVehicleID);
        }


        // Regular Methods

        public void UpdateIDList()
        {
            vehicleList.Clear();
            vehicleList = sqlHandler.GetUserIDList("ID_VEHICLE", "VEHICLE");
            UpdateVehicleID = vehicleList;
            DeleteVehicleID = vehicleList;
            StatusVehicleID = vehicleList;

        }


    }
}
