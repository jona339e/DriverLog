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

        [ObservableProperty]
        public string? createUsername;

        [ObservableProperty]
        public string? createPassword;

        [ObservableProperty]
        public bool createIsAdmin = false;

        [RelayCommand]
        public void OnCreateUser()
        {

            // sets username and password input to properties in the model class
            UserModel um = new();
            um.Username = createUsername;
            um.Password = createPassword;
            um.IsAdmin = createIsAdmin;
            um.Date = DateTime.Now;

            SqlHandler sqlHandler= new SqlHandler();
            sqlHandler.CreateUser( um );

            // sets the user input text boxes to an empty string, so they are ready for a new input
            CreateUsername = "";
            CreatePassword = "";
            CreateIsAdmin= false;

            MessageBox.Show($"User {um.Username} Created. At: {um.Date}");
        }


    }
}
