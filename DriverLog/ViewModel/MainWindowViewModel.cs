using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.View;
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
        private string? userName;

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
        private string? passWord;



        // Instead of the code below, I can use the [RelayCommand] attribute on my method to invoke it on

        //public ICommand? LoginCommand { get; }


        //public MainWindowViewModel()
        //{
        //    LoginCommand = new RelayCommand(OnLogin);
        //}

        [RelayCommand]
        public void OnLogin()
        {
            if (UserName == "Jonas" && passWord =="123")
            {
                DriversLog dl = new DriversLog();
                dl.Show();
                notifyWindowToClose();
            }
            
        }


        // Creates a message with the text "CloseWindow" and sends it to the view.
        public void notifyWindowToClose()
        {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage("CloseWindow"));
        }

    }
}
