using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace DriverLog.Model
{
    public class SqlHandler
    {
        // skole database -- maskine hedder 003
        private const string? sqlconn = @"Data Source=DESKTOP-CGQRRVF\SQLEXPRESS;Initial Catalog=KLOG;User ID=sa;Password=Passw0rd";
        private SqlCommand? cmnd;
        private SqlDataReader? dr;
        private SqlDataAdapter? da = new();
        private SqlConnection? conn = new(sqlconn);


        public List<int> GetUserIDList()
        {
            List<int> list = new List<int>();
            conn.Open();
            {
                cmnd = new("SELECT ID_USER FROM [USER]", conn);

                dr = cmnd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr.GetInt32(0));
                }
            }
            cmnd.Dispose();
            dr.Close();
            conn.Close();
            return list;
        }

        public int GetIdFromUsername()
        {
            int userID = 0;

            conn.Open();
            {
                cmnd = new("SELECT ID_USER FROM [USER] WHERE Username = @LoggedUser", conn);
                cmnd.Parameters.AddWithValue("@LoggedUser", GlobalUsername.Username);

                dr = cmnd.ExecuteReader();
                while (dr.Read())
                {
                    userID= dr.GetInt32(0);
                }
            }
            dr.Close();
            cmnd.Dispose();
            conn.Close();

            return userID;
        }

        // Checks if username and password exist in database
        // return an array of 2 bools the 1st bool represents the match between username and password in the database
        // the 2nd bool represents if it the user is admin or not
        public bool[] LoginCheck(string username, string password)
        {
            bool[] list = new bool[2];
            try
            {
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
                                list[0] = true;
                                list[1] = true;
                            }
                            else
                            {
                                list[0] = true;
                                list[1] = false;
                            }
                        }
                        else
                        {
                            list[0] = false;
                            list[1] = false;

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                EventLogPageViewModel ELog = new();
                ELog.LogEvent(ex.ToString());
                MessageBox.Show($"Unexpected Error occured: {ex.ToString()}");
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }

            return list;
        }



        


        // ADMIN - USER METHODS
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

        public void UpdateUser(UserModel um, int? userID)
        {
            conn.Open();
            {
                cmnd = new("UPDATE [USER] SET USERNAME = @UpdateUsername, PASSWORD = @UpdatePassword, ISADMIN = @UpdateIsAdmin WHERE ID_USER = @UserID",conn);
                cmnd.Parameters.AddWithValue("@UpdateUsername", um.Username);
                cmnd.Parameters.AddWithValue("@UpdatePassword", um.Password);
                cmnd.Parameters.AddWithValue("@UpdateIsAdmin", um.IsAdmin);
                cmnd.Parameters.AddWithValue("@UserID", userID);

                da.UpdateCommand = cmnd;
                da.UpdateCommand.ExecuteNonQuery();
            }
            da.Dispose();
            cmnd.Dispose();
            conn.Close();

        }

        public UserModel GetUserData(int? userID)
        {
            UserModel um = new();

            conn.Open();
            {
                cmnd = new("SELECT USERNAME, PASSWORD, ISADMIN FROM [USER] WHERE ID_USER = @USERID", conn);
                cmnd.Parameters.AddWithValue("@USERID", userID);

                dr = cmnd.ExecuteReader();
                while (dr.Read())
                {
                    um.Username = dr.GetString(0);
                    um.Password= dr.GetString(1);
                    um.IsAdmin= dr.GetBoolean(2);
                }
            }
            dr.Close();
            cmnd.Dispose();
            conn.Close();
            return um;
        }

        public void DeleteUser(int? userID)
        {
            conn.Open();
            {
                cmnd = new("DELETE FROM [USER] WHERE ID_USER = @USERID", conn);
                cmnd.Parameters.AddWithValue("@USERID", userID);

                da.DeleteCommand = cmnd;
                da.DeleteCommand.ExecuteNonQuery();

            }
            cmnd.Dispose();
            da.Dispose();
            conn.Close();

        }




        // ADMIN - VEHICLE METHODS

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

        public void UpdateVehicle(VehicleModel vm, int? ID)
        {
            conn.Open();
            {

                cmnd = new("UPDATE VEHICLE SET MODEL = @UpdateModel, PLATE = @UpdatePlate WHERE ID_VEHICLE = @ID", conn);
                cmnd.Parameters.AddWithValue("@UpdateModel", vm.Model);
                cmnd.Parameters.AddWithValue("@UpdatePlate", vm.Plate);
                cmnd.Parameters.AddWithValue("@ID", ID);

                da.UpdateCommand = cmnd;
                da.UpdateCommand.ExecuteNonQuery();

            }
            da.Dispose();
            cmnd.Dispose();
            conn.Close();
        }

        public VehicleModel GetVehicleData(int? vehicleID)
        {
            VehicleModel vm = new();

            conn.Open();
            {
                cmnd = new("SELECT Model, Plate FROM Vehicle WHERE ID_VEHICLE = @VEHICLEID", conn);
                cmnd.Parameters.AddWithValue("@VEHICLEID", vehicleID);

                dr = cmnd.ExecuteReader();
                while (dr.Read())
                {
                    vm.Model= dr.GetString(0);
                    vm.Plate= dr.GetString(1);
                    
                }
            }
            cmnd.Dispose();
            dr.Close();
            conn.Close();
            return vm;
        }

        public void UpdateStatus(string? status, int? ID)
        {
            conn.Open();
            {

                cmnd = new("UPDATE VEHICLE SET STATUS = @UpdateStatus WHERE ID_VEHICLE = @ID", conn);
                cmnd.Parameters.AddWithValue("@UpdateStatus", status);
                cmnd.Parameters.AddWithValue("@ID", ID);

                da.UpdateCommand = cmnd;
                da.UpdateCommand.ExecuteNonQuery();

            }
            da.Dispose();
            cmnd.Dispose();
            conn.Close();
        }


        public List<int> GetVehicleIDList()
        {
            List<int> list = new List<int>();
            conn.Open();
            {
                cmnd = new("SELECT ID_Vehicle FROM Vehicle", conn);

                dr = cmnd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr.GetInt32(0));
                }
            }
            cmnd.Dispose();
            dr.Close();
            conn.Close();
            return list;
        }

        public void DeleteVehicle(int? vehicleID)
        {
            conn.Open();
            {
                cmnd = new("DELETE FROM Vehicle WHERE ID_Vehicle = @ID", conn);
                cmnd.Parameters.AddWithValue("@ID", vehicleID);

                da.DeleteCommand = cmnd;
                da.DeleteCommand.ExecuteNonQuery();

            }
            cmnd.Dispose();
            da.Dispose();
            conn.Close();
        }

        public string GetStatusData(int? statusSelectedVehicleID)
        {
            string status = "";
            conn.Open();
            {
                cmnd = new("SELECT STATUS FROM VEHICLE WHERE ID_VEHICLE = @ID", conn);
                cmnd.Parameters.AddWithValue("@ID",statusSelectedVehicleID);

                dr= cmnd.ExecuteReader();
                while (dr.Read()) 
                {
                    status = dr.GetString(0);
                }
            }
            dr.Close();
            cmnd.Dispose();
            conn.Close();
            return status;
        }

        public List<EventLogDTO> GetEvents()
        {

            List<EventLogDTO> ELs = new();
            try
            {

                conn.Open();
                {
                    cmnd = new("SELECT * FROM EventView");
                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        EventLogDTO EL = new();
                        EL.Event_Entry = dr.GetString(0);
                        EL.Username = dr.GetString(1);
                        EL.Date = dr.GetDateTime(2);
                        ELs.Add(EL);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error occured: {ex.ToString()}");

            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }

            return ELs;

        }



        // EventLog
        public void AddEventLog(EventLogModel elm)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("INSERT INTO EVENT_LOG VALUES (@EVENTENTRY, @DateNow, @USERID)", conn);
                    cmnd.Parameters.AddWithValue("@EVENTENTRY", elm.Event_Entry);
                    cmnd.Parameters.AddWithValue("@DateNow", elm.Date);
                    cmnd.Parameters.AddWithValue("@USERID", elm.UserID);

                    da.InsertCommand = cmnd;
                    da.InsertCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Unexpected Error occured: {ex.ToString()}");
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
            

        }

    }
}
