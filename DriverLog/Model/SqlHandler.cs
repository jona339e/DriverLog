using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLog.Model
{
    public class SqlHandler
    {
        private const string? sqlconn = @"Data Source=DESKTOP-CGQRRVF\SQLEXPRESS;Initial Catalog=KLOG;User ID=sa;Password=Passw0rd";
        private SqlCommand? cmnd;
        private SqlDataReader? dr;
        private SqlDataAdapter? da = new();
        private SqlConnection? conn = new(sqlconn);


        public void GetUserIDList()
        {

        }

        public void LoginCheck(string username, string password)
        {
            // Sql open, sql select command and sql injection prevention

            conn.Open();
            {
                cmnd = new("SELECT Username, [Password], IsAdmin FROM [USER] where Username = @LoginUserName", conn);
                cmnd.Parameters.AddWithValue("@LoginUserName", username);
                cmnd.Parameters.AddWithValue("@LoginUserPsw", password);

                dr = cmnd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetString(0) == username && dr.GetString(1) == password)
                    {

                        if (dr.GetBoolean(2))
                        {
                            // message to login admin user
                            WeakReferenceMessenger.Default.Send(new LoginMessage("IsAdmin"));
                        }
                        else
                        {
                            // message to login normal user
                            WeakReferenceMessenger.Default.Send(new LoginMessage("IsUser"));
                        }
                    }
                    else
                    {
                        // message to login wrong credentials
                        WeakReferenceMessenger.Default.Send(new LoginMessage("WrongCredentials"));

                    }
                }
            }
            dr.Close();
            cmnd.Dispose();
            conn.Close();
        }


        public void CreateUser(UserModel um)
        {
            conn.Open();
            {
                cmnd = new("INSERT INTO [USER] VALUES (@CreateUser, @CreatePassword, @CreateDate, @CreateIsAdmin)", conn);
                cmnd.Parameters.AddWithValue("@CreateUser", um.Username);
                cmnd.Parameters.AddWithValue("@CreatePassword", um.Password);
                cmnd.Parameters.AddWithValue("@CreateDate", um.Date);
                cmnd.Parameters.AddWithValue("@CreateIsAdmin", um.IsAdmin);

                da.InsertCommand = cmnd;
                da.InsertCommand.ExecuteNonQuery();

            }
            da.Dispose();
            cmnd.Dispose();
            conn.Close();

        }        

        public void UpdateUser()
        {

        }


        public void CreateVehicle(VehicleModel vm)
        {
            conn.Open();
            {
                cmnd = new("INSERT INTO Vehicle VALUES (@CreateModel, @CreatePlate, @CreateStatus)", conn);
                cmnd.Parameters.AddWithValue("@CreateModel", vm.Model);
                cmnd.Parameters.AddWithValue("@CreatePlate", vm.Plate);
                cmnd.Parameters.AddWithValue("@CreateStatus", vm.Status);


                da.InsertCommand = cmnd;
                da.InsertCommand.ExecuteNonQuery();
            }
            da.Dispose();
            cmnd.Dispose();
            conn.Close();

        }

        


    }
}
