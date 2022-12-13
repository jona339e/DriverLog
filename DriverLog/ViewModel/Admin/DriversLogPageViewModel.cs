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
            SqlHandler sqlHandler = new SqlHandler();
            DriveUserList = sqlHandler.GetUsernameList();
            DrivePlateList = sqlHandler.GetPlateList();
        }


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
