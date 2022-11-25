using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Messages
{
    internal class AdminWindowControlMessage : ValueChangedMessage<string>
    {
        public AdminWindowControlMessage(string value) : base(value)
        {
        }
    }
}
