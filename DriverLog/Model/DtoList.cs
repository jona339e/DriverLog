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

        public List<LogModel> LogList = new();
        public  List<UserModel> UserList = new();
        public List<VehicleModel> VehicleList = new();
        public List<EventLogModel> Event_LogList = new();
        public List<AbsenceModel> AbsenceList = new();
    }
}
