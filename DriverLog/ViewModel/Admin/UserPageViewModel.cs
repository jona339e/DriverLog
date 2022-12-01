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
    public partial class UserPageViewModel : ObservableObject
    {

        public SqlHandler sqlHandler = new SqlHandler();

        public List<int> IDLIST { get; set; }


        // Create User Field
        [ObservableProperty]
        public string? createUsername;

        [ObservableProperty]
        public string? createPassword;

        [ObservableProperty]
        public bool createIsAdmin = false;




        // Edit/Update User Field

        [ObservableProperty]
        public List<int>? updateUserID;

        [ObservableProperty]
        public int? updateSelectedUserID;

        [ObservableProperty]
        public string? updateUsername;

        [ObservableProperty]
        public string? updatePassword;


        [ObservableProperty]
        public bool updateIsAdmin = false;




        // Delete User Field


        [ObservableProperty]
        public List<int>? deleteUserID;

        [ObservableProperty]
        public int? deleteSelectedUserID;

        [ObservableProperty]
        public string? deleteUsername;

        [ObservableProperty]
        public string? deletePassword;

        [ObservableProperty]
        public bool deleteIsAdmin;








        // Constructor

        public UserPageViewModel()
        {

            IDLIST = sqlHandler.GetUserIDList();

            UpdateMyIDS();
        }



        // Command Bound Methods

        [RelayCommand]
        public void OnCreateUser()
        {

            // sets username and password input to properties in the model class
            UserModel um = new();
            um.Username = createUsername;
            um.Password = createPassword;
            um.IsAdmin = createIsAdmin;
            um.Date = DateTime.Now;

            
            sqlHandler.CreateUser( um );

            // sets the user input text boxes to an empty string, so they are ready for a new input
            CreateUsername = "";
            CreatePassword = "";
            CreateIsAdmin= false;

            MessageBox.Show($"User {um.Username} Created. At: {um.Date}");
            UpdateMyIDS();
        }


        [RelayCommand]
        public void OnUpdateUser()
        {
            UserModel um = new();
            um.Username = UpdateUsername;
            um.Password = UpdatePassword;
            um.IsAdmin = UpdateIsAdmin;

            sqlHandler.UpdateUser(um, UpdateSelectedUserID);

            UpdateIsAdmin = false;
            UpdatePassword = string.Empty;
            UpdateUsername = string.Empty;

            MessageBox.Show($"User Edited to : {um.Username}");
            UpdateMyIDS();

        }

        [RelayCommand]
        public void OnDeleteShowData()
        {
            UserModel um = new();
            um = sqlHandler.GetUserData(DeleteSelectedUserID);

            DeleteUsername = um.Username;
            DeletePassword = um.Password;
            DeleteIsAdmin = um.IsAdmin;
        }

        [RelayCommand]
        public void OnDeleteUser()
        {
            sqlHandler.DeleteUser(DeleteSelectedUserID);
            MessageBox.Show($"User {DeleteUsername} Deleted");
            UpdateMyIDS();
        }


        public void myMethod()
        {
            MessageBox.Show(DeleteUsername);
        }

        // Methods

        private void UpdateMyIDS()
        {
            IDLIST.Clear();
            IDLIST = sqlHandler.GetUserIDList();
            UpdateUserID = IDLIST;
            DeleteUserID = IDLIST;
        }


    }
}
