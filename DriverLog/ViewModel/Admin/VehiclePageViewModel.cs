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
            vehicleList = sqlHandler.GetUserIDList();
            UpdateIDList();
        }


        // Bound Methods

        [RelayCommand]
        public void OnCreateVehicle()
        {
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.Model = CreateModel;
            vehicleModel.Plate = CreatePlate;

            sqlHandler.CreateVehicle(vehicleModel);

            SetValuesToEmpty();

            MessageBox.Show($"Oprettede {vehicleModel.Model} med Nr. Plade: {vehicleModel.Plate}");
            UpdateIDList();

        }

        [RelayCommand]
        public void OnUpdateVehicle() 
        {
            VehicleModel vehicleModel = new();
            vehicleModel.Model = UpdateModel;
            vehicleModel.Plate = UpdatePlate;

            sqlHandler.UpdateVehicle(vehicleModel, updateSelectedVehicleID);
            MessageBox.Show($"Ændret til {UpdateModel} - {UpdatePlate}");


            SetValuesToEmpty();
        }

        [RelayCommand]
        public void OnDeleteShowVehicleData()
        {
            if (DeleteSelectedVehicleID != null)
            {


                VehicleModel vehicleModel = new();
                vehicleModel = sqlHandler.GetVehicleData(DeleteSelectedVehicleID);

                DeleteModel = vehicleModel.Model;
                DeletePlate = vehicleModel.Plate;


            }
            else
            {
                SetValuesToEmpty();
            }
        }

        [RelayCommand]
        public void OnShowCurrentStatus()
        {
            if (StatusSelectedVehicleID != null)
            {
                string currentStatus = sqlHandler.GetStatusData(StatusSelectedVehicleID);

                ChangeStatus = currentStatus;
            }
            else
            {
                SetValuesToEmpty();
            }

        }

        [RelayCommand]
        public void OnUpdateShowVehicleData()
        {
            if (UpdateSelectedVehicleID != null)
            {
                VehicleModel vehicleModel = new();
                vehicleModel = sqlHandler.GetVehicleData(UpdateSelectedVehicleID);

                UpdateModel = vehicleModel.Model;
                UpdatePlate = vehicleModel.Plate;

            }
            else
            {
                SetValuesToEmpty();
            }
        }


        [RelayCommand]
        public void OnDeleteVehicle()
        {

            sqlHandler.DeleteVehicle(DeleteSelectedVehicleID);
            MessageBox.Show($"Vehicle {DeleteModel} - {DeletePlate} Deleted");

            UpdateIDList();

        }


        [RelayCommand]
        public void OnChangeStatus()
        {
            sqlHandler.UpdateStatus(ChangeStatus, statusSelectedVehicleID);

            MessageBox.Show($"Oprettet Status: {ChangeStatus}");

            SetValuesToEmpty();

        }


        // Regular Methods

        public void UpdateIDList()
        {
            vehicleList.Clear();
            vehicleList = sqlHandler.GetVehicleIDList();
            UpdateVehicleID = vehicleList;
            DeleteVehicleID = vehicleList;
            StatusVehicleID = vehicleList;

        }


        private void SetValuesToEmpty()
        {
            UpdateSelectedVehicleID = null;
            UpdateModel = string.Empty;
            UpdatePlate = string.Empty;
            
            DeleteSelectedVehicleID = null;
            DeleteModel = string.Empty;
            DeletePlate = string.Empty;

            StatusSelectedVehicleID = null;
            ChangeStatus = string.Empty;
        }


    }
}
