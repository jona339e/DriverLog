using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DriverLog.View.User
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {
        public UserDashboard()
        {
            DataContext = new UserDashboardViewModel();
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<UserWindowControlMessage>(this, (reciver, message) =>
            {
                if (message.Value == "CloseWindow")
                {
                    this.Close();
                }
            });            

            WeakReferenceMessenger.Default.Register<DriveLogWindowControlMessage>(this, (reciver, message) =>
            {
                if (message.Value == "OpenWindow")
                {
                    AddDriveLog ADL = new();
                    ADL.Show();
                }
            });

        }
    }
}
