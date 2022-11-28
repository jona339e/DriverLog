using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class UserModel
    {

        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdmin { get; set; }

    }
}
