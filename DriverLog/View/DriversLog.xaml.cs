using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
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
    /// Interaction logic for DriversLog.xaml
    /// </summary>
    public partial class DriversLog : Window
    {
        public DriversLog()
        {
            DataContext = new DriversLogViewModel();
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<ChangeVisibilityMessage>(this, (reciver, message) =>
            {
                if (message.Value == "Hide")
                {
                    this.Visibility= Visibility.Hidden;
                }
                else if (message.Value == "Show")
                {
                    this.Visibility = Visibility.Visible;
                }
            });

            WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (reciver, message) =>
            {
                if (message.Value == "CloseMenuWindow")
                {
                    this.Close();
                }
            });
        }
    }
}
