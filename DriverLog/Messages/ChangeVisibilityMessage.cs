using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Messages
{
    public class ChangeVisibilityMessage : ValueChangedMessage<string>
    {
        public ChangeVisibilityMessage(string value) : base(value)
        {
        }
    }
}
