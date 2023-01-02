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
        EventLogSubViewModel elsvm = new();

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

        [ObservableProperty]
        List<VehicleModel> vehicleView; // Id - Model - Pate - status



        // Constructor
        public VehiclePageViewModel()
        {
            vehicleList = sqlHandler.GetUserIDList();
            UpdateGrid();
            UpdateIDList();
        }


        // Bound Methods

        [RelayCommand]
        public void OnCreateVehicle()
        {
            // creates vehicle in database from input
            // triggers on button press
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.Model = CreateModel;
            vehicleModel.Plate = CreatePlate;

            sqlHandler.CreateVehicle(vehicleModel);

            SetValuesToEmpty();

            MessageBox.Show($"Oprettede {vehicleModel.Model} med Nr. Plade: {vehicleModel.Plate}");
            UpdateIDList();
            elsvm.LogEvent("Vehicle Created", LogLevel.Information);
            UpdateGrid();

        }

        [RelayCommand]
        public void OnUpdateVehicle() 
        {
            // updates vehicle in database on button press
            VehicleModel vehicleModel = new();
            vehicleModel.Model = UpdateModel;
            vehicleModel.Plate = UpdatePlate;

            sqlHandler.UpdateVehicle(vehicleModel, updateSelectedVehicleID);
            MessageBox.Show($"Ændret til {UpdateModel} - {UpdatePlate}");


            SetValuesToEmpty();
            elsvm.LogEvent("Vehicle Edited", LogLevel.Information);

            UpdateGrid();
        }

        [RelayCommand]
        public void OnDeleteShowVehicleData()
        {

            if (DeleteSelectedVehicleID != null)
            {
                // populates textboxes with the data recieved from database. triggers on change in selection in the delete section.

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
            // gets status of vehicle from database and displays it in the textbox
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
            // Populates data from database corresponding to vehicle ID
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
            // deletes vehicle in database & updates list of IDs
            sqlHandler.DeleteVehicle(DeleteSelectedVehicleID);
            MessageBox.Show($"Vehicle {DeleteModel} - {DeletePlate} Deleted");

            UpdateIDList();
            elsvm.LogEvent("Vehicle Deleted", LogLevel.Information);
            UpdateGrid();

        }


        [RelayCommand]
        public void OnChangeStatus()
        {
            // creates or edits status for vehicle

            sqlHandler.UpdateStatus(ChangeStatus, statusSelectedVehicleID);

            MessageBox.Show($"Oprettet Status: {ChangeStatus}");

            SetValuesToEmpty();
            elsvm.LogEvent("Vehicle Status Created", LogLevel.Information);
            UpdateGrid();

        }


        // Regular Methods

        public void UpdateGrid()
        {
            VehicleView = sqlHandler.GetVehicleGrid();
        }

        public void UpdateIDList()
        {
            // clears ID list and re populate them from the database
            vehicleList.Clear();
            vehicleList = sqlHandler.GetVehicleIDList();
            UpdateVehicleID = vehicleList;
            DeleteVehicleID = vehicleList;
            StatusVehicleID = vehicleList;

        }

        private void SetValuesToEmpty()
        {
            // sets values to null or empty for all input fields

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
