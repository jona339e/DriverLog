using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DriverLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // DataContext sets the binding source to the modelview so the view knows where to send data.
            DataContext = new MainWindowViewModel();

            InitializeComponent();

            // Registers a message of type CloseWindowMessage, if the message value is "CloseWindow" the window is then closed.
            WeakReferenceMessenger.Default.Register<MainWindowControlMessage>(this, (reciver, message) =>
            {
                if (message.Value == "CloseWindow")
                {
                    this.Close();
                }
            });
        }

        


        // As this is stil the view, we have to send data from here to the ViewModel.
        // this is primarily done in the xaml window, where bindings are made to properties in the ViewModel

    }
}
