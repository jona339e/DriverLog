using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.View.Admin;
using DriverLog.ViewModel;
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

namespace DriverLog.View
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {

            DataContext = new AdminDashboardViewModel();
            InitializeComponent();

            // Shows HomePage on the "Main" Frame
            Main.Content = new HomePage();



            // Gets messages / notifications and displays a page, on the main frame, based on the message value

            MessageRegisterer();
            RegisterLogOut();
        }

        private void RegisterLogOut()
        {
            WeakReferenceMessenger.Default.Register<AdminWindowControlMessage>(this, (reciever, message) =>
            {
                if (message.Value == "CloseWindow")
                {
                    this.Close();
                }
            });
        }

        private void MessageRegisterer()
        {
            WeakReferenceMessenger.Default.Register<PageMessage>(this, (reciver, message) =>
            {
                if (message.Value == "Home")
                {
                    Main.Content = new HomePage();
                }
                else if (message.Value == "User")
                {
                    Main.Content = new UserPage();
                }
                else if (message.Value == "Absence")
                {
                    Main.Content = new AbsencePage();
                }
                else if (message.Value == "Vehicle")
                {
                    Main.Content = new VehiclePage();
                }
                else if (message.Value == "DriversLog")
                {
                    Main.Content = new DriversLogPage();
                }
                else if (message.Value == "EventLog")
                {
                    Main.Content = new EventLogPage();
                }
            });
        }
    }
}
