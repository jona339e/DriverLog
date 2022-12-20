using CommunityToolkit.Mvvm.ComponentModel;
using DriverLog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DriverLog.Model.GlobalUsername;


namespace DriverLog.ViewModel.Admin
{
    public partial class DriversLogPageViewModel : ObservableObject
    {
        // properties for lists of users and plates, selected user and plates, and lists to populate the grids with.
        [ObservableProperty]
        public List<String> driveUserList;

        [ObservableProperty]
        public string selectedUser;        
        
        [ObservableProperty]
        public List<String> drivePlateList;

        [ObservableProperty]
        public string selectedPlate;

        [ObservableProperty]
        public List<DriveLogDTO> userLogData;

        [ObservableProperty]
        public List<DriveLogDTO> plateLogData;

        
        public DriversLogPageViewModel()
        {
            // constructor that gets 2 lists from database and sets them to lists that are observable
            SqlHandler sqlHandler = new SqlHandler();
            DriveUserList = sqlHandler.GetUsernameList();
            DrivePlateList = sqlHandler.GetPlateList();
        }

        //method that changes the grid based on what user is selected
        //this method triggers whenever a user is changed in the combobox in DriversLogPage
        //previously I have been using i:interactivity in the view to make this happen
        //but then I found community toolkits comes with a function for it.
        partial void OnSelectedUserChanged(string value)
        {
            if (SelectedUser != null)
            {
                SqlHandler sqlHandler = new();
                UserLogData = sqlHandler.GetDriveLog(SelectedUser);
            }
        }

        //partial void OnDrivePlateListChanged(List<string> value)
        //{

        //}

    }
}
