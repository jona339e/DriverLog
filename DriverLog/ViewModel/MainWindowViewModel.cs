using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.Model;
using DriverLog.View;
using DriverLog.View.User;
using DriverLog.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static DriverLog.Model.GlobalUsername;

namespace DriverLog.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        EventLogSubViewModel evlpvm = new();

        [ObservableProperty]
        private string? userName = "Jonas";

        // Using ObservableProperty attribute generates the following

        //public string? UserName
        //{
        //    get { return userName; }
        //    set 
        //    { 
        //        userName = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(userName)));
        //    }
        //}

        //public event PropertyChangedEventHandler? PropertyChanged;

        [ObservableProperty]
        private string? passWord = "123";



        // Instead of the code below, I can use the [RelayCommand] attribute on my method to invoke it on

        //public ICommand? LoginCommand { get; }


        //public MainWindowViewModel()
        //{
        //    LoginCommand = new RelayCommand(OnLogin);
        //}


        [RelayCommand]
        public void OnLogin()
        {
            bool[] bools = new bool[2];
            SqlHandler sqlh = new();
            GlobalUsername.Username = UserName;


            bools = sqlh.LoginCheck(UserName, PassWord);
            if (bools[0] && bools[1])
            {
                // message to login admin user
                WeakReferenceMessenger.Default.Send(new LoginMessage("IsAdmin"));
                evlpvm.LogEvent("Login", 0);
            }
            else if (bools[0] && !bools[1])
            {
                // message to login normal user
                WeakReferenceMessenger.Default.Send(new LoginMessage("IsUser"));
                evlpvm.LogEvent("Login", 0);
            }
            else
            {
                // message to login wrong credentials
                WeakReferenceMessenger.Default.Send(new LoginMessage("WrongCredentials"));
                PassWord = "";
                UserName = string.Empty;
            }


        }

        // Creates a message with the text "CloseWindow" and sends it to the view.
        public void notifyWindowToClose()
        {
            WeakReferenceMessenger.Default.Send(new MainWindowControlMessage("CloseWindow"));
        }

    }
}
