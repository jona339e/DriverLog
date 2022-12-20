using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddDriveLog.xaml
    /// </summary>
    public partial class AddDriveLog : Window
    {
        public AddDriveLog()
        {
            DataContext = new AddDriveLogViewModel();
            InitializeComponent();


            // closes window if message is recieved.
            WeakReferenceMessenger.Default.Register<DriveLogWindowControlMessage>(this, (reciver, message) =>
            {
                if (message.Value == "CloseWindow")
                {
                    this.Close();
                }
            });
        }

        // the priveiwtextinput can't be used on strings
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // regex that dictates input can only be numbers 0-9. if another key is pressed it won't register as an input.
            Regex rx = new("[^0-9]+");
            e.Handled = rx.IsMatch(e.Text);
        }

    }
}
