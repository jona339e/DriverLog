using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.Model;
using DriverLog.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DriverLog.ViewModel.User
{
    public partial class AddDriveLogViewModel : ObservableObject
    {
        public SqlHandler sqlHandler = new();

        // properties bound to the input fileds of AddDriveLog (view)

        [ObservableProperty]
        List<string> plateList; // list of plates are bound to the combobox

        [ObservableProperty]
        string selectedPlate;

        [ObservableProperty]
        string username = GlobalUsername.Username;

        [ObservableProperty]
        DateTime datePick = DateTime.Now;

        [ObservableProperty]
        string startTime = "HH:mm";

        [ObservableProperty]
        string endTime = "HH:mm";

        [ObservableProperty]
        int distance;

        public AddDriveLogViewModel()
        {
            PlateList = sqlHandler.GetPlateList(); // gets list of all plates from database
        }

        [RelayCommand]
        public void OnAddDriveCancel()
        {
            // closes window if cancel button is pressed
            NotifyAddDriveWindowToClose();
        }

        [RelayCommand]
        public void OnAddDriveConfirm()
        {
            // adds inputs as values to the DriveLogDTO object. then sends it to the database


            // passing strings to date time in hour:min format
            DateTime TimeStart = DateTime.ParseExact(StartTime, "HH:mm", CultureInfo.CurrentCulture) ;
            DateTime TimeEnd = DateTime.ParseExact(EndTime, "HH:mm", CultureInfo.CurrentUICulture) ;

            DriveLogDTO AddDTO = new();
            AddDTO.Username = username;
            AddDTO.Plate = selectedPlate;
            AddDTO.Date = datePick;
            AddDTO.StartTime = TimeStart;
            AddDTO.EndTime = TimeEnd;
            AddDTO.Distance = distance;

            sqlHandler.CreateDriveLog(AddDTO);

            NotifyAddDriveWindowToClose();
        }

        // closes window
        public void NotifyAddDriveWindowToClose()
        {
            WeakReferenceMessenger.Default.Send(new DriveLogWindowControlMessage("CloseWindow"));
        }


    }
}
