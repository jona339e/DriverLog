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

        List<Log> LogList = new();
        List<User> UserList = new();
        List<Vehicle> VehicleList = new();
        List<Event_Log> Event_LogList = new();
        List<Absence> AbsenceList = new();
    }
}
