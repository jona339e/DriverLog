using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class DtoList
    {
        // Create Lists of different objects to send over for database insertion

        public List<Log> LogList = new();
        public  List<UserModel> UserList = new();
        public List<Vehicle> VehicleList = new();
        public List<Event_Log> Event_LogList = new();
        public List<Absence> AbsenceList = new();
    }
}
