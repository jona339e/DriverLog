using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Messages
{
    internal class PageMessage : ValueChangedMessage<string>
    {
        public PageMessage(string value) : base(value)
        {
        }
    }
}
