using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.Model;
using DriverLog.View;
using DriverLog.View.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DriverLog.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {

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

        // obsolete code


        // Instead of the code below, I can use the [RelayCommand] attribute on my method to invoke it on

        //public ICommand? LoginCommand { get; }


        //public MainWindowViewModel()
        //{
        //    LoginCommand = new RelayCommand(OnLogin);
        //}


        [RelayCommand]
        public void OnLogin()
        {

            SqlHandler sqlh = new();

            sqlh.LoginCheck(UserName, PassWord);



            //if (UserName == "Jonas" && passWord == "123")
            //{
            //    AdminDashboard AD = new AdminDashboard();
            //    AD.Show();
            //    notifyWindowToClose();
            //}
            //else if (UserName == "Jonas" && passWord == "1234")
            //{
            //    UserDashboard UD = new();
            //    UD.Show();
            //    notifyWindowToClose();
            //}

        }



        public void WrongCredentials()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this, (reciver, message) =>
            {
                if (message.Value == "WrongCredentials")
                {
                    PassWord = "";
                    UserName = string.Empty;
                }
            });
        }


        // Creates a message with the text "CloseWindow" and sends it to the view.
        public void notifyWindowToClose()
        {
            WeakReferenceMessenger.Default.Send(new MainWindowControlMessage("CloseWindow"));
        }

    }
}
