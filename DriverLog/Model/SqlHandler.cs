using CommunityToolkit.Mvvm.Messaging;
using DriverLog.Messages;
using DriverLog.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT ID_USER FROM [USER]", conn);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(dr.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);

            }
            finally
            {
                cmnd.Dispose();
                dr.Close();
                conn.Close();
            }
            
            return list;
        }

        public int GetIdFromUsername()
        {
            // connection is open???
            int userID = 0;
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT ID_USER FROM [USER] WHERE Username = @LoggedUser", conn);
                    cmnd.Parameters.AddWithValue("@LoggedUser", GlobalUsername.Username);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        userID = dr.GetInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
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
                ErrorCatch(ex);
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
            try
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
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }        

        public void UpdateUser(UserModel um, int? userID)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("UPDATE [USER] SET USERNAME = @UpdateUsername, PASSWORD = @UpdatePassword, ISADMIN = @UpdateIsAdmin WHERE ID_USER = @UserID", conn);
                    cmnd.Parameters.AddWithValue("@UpdateUsername", um.Username);
                    cmnd.Parameters.AddWithValue("@UpdatePassword", um.Password);
                    cmnd.Parameters.AddWithValue("@UpdateIsAdmin", um.IsAdmin);
                    cmnd.Parameters.AddWithValue("@UserID", userID);

                    da.UpdateCommand = cmnd;
                    da.UpdateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally 
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }

        public UserModel GetUserData(int? userID)
        {
            UserModel um = new();

            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT USERNAME, PASSWORD, ISADMIN FROM [USER] WHERE ID_USER = @USERID", conn);
                    cmnd.Parameters.AddWithValue("@USERID", userID);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        um.Username = dr.GetString(0);
                        um.Password = dr.GetString(1);
                        um.IsAdmin = dr.GetBoolean(2);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
            return um;
        }

        public void DeleteUser(int? userID)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("DELETE FROM [USER] WHERE ID_USER = @USERID", conn);
                    cmnd.Parameters.AddWithValue("@USERID", userID);

                    da.DeleteCommand = cmnd;
                    da.DeleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }


        // ADMIN - VEHICLE METHODS

        public void CreateVehicle(VehicleModel vm)
        {
            try
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
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }

        public void UpdateVehicle(VehicleModel vm, int? ID)
        {
            try
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
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }

        public VehicleModel GetVehicleData(int? vehicleID)
        {
            VehicleModel vm = new();
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT Model, Plate FROM Vehicle WHERE ID_VEHICLE = @VEHICLEID", conn);
                    cmnd.Parameters.AddWithValue("@VEHICLEID", vehicleID);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        vm.Model = dr.GetString(0);
                        vm.Plate = dr.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                cmnd.Dispose();
                dr.Close();
                conn.Close();
            }
            return vm;
        }

        public void UpdateStatus(string? status, int? ID)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("UPDATE VEHICLE SET STATUS = @UpdateStatus WHERE ID_VEHICLE = @ID", conn);
                    cmnd.Parameters.AddWithValue("@UpdateStatus", status);
                    cmnd.Parameters.AddWithValue("@ID", ID);

                    da.UpdateCommand = cmnd;
                    da.UpdateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }

        public List<int> GetVehicleIDList()
        {
            List<int> list = new List<int>();
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT ID_Vehicle FROM Vehicle", conn);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(dr.GetInt32(0));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                cmnd.Dispose();
                dr.Close();
                conn.Close();
            }
            return list;
        }

        public void DeleteVehicle(int? vehicleID)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("DELETE FROM Vehicle WHERE ID_Vehicle = @ID", conn);
                    cmnd.Parameters.AddWithValue("@ID", vehicleID);

                    da.DeleteCommand = cmnd;
                    da.DeleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                cmnd.Dispose();
                da.Dispose();
                conn.Close();
            }
        }

        public string GetStatusData(int? statusSelectedVehicleID)
        {
            string status = "";
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT STATUS FROM VEHICLE WHERE ID_VEHICLE = @ID", conn);
                    cmnd.Parameters.AddWithValue("@ID", statusSelectedVehicleID);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        status = dr.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
            return status;
        }



        // EventLog

        public void AddEventLog(EventLogModel elm)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("INSERT INTO EVENT_LOG VALUES (@EVENTENTRY, @logLevel, @DateNow, @USERID)", conn);
                    cmnd.Parameters.AddWithValue("@logLevel", Convert.ToInt32(elm.Loglevel));
                    cmnd.Parameters.AddWithValue("@EVENTENTRY", elm.Event_Entry);
                    cmnd.Parameters.AddWithValue("@DateNow", elm.Date);
                    cmnd.Parameters.AddWithValue("@USERID", elm.UserID);

                    da.InsertCommand = cmnd;
                    da.InsertCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error occured: {ex.Message}");
            }
            finally
            {
                da.Dispose();
                cmnd.Dispose();
                conn.Close();
            }
        }


        public List<EventLogDTO> GetEventLogList()
        {
            List<EventLogDTO> eventLogDTO= new List<EventLogDTO>();
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT * FROM EventView ORDER BY [DATE] DESC", conn);
                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        EventLogDTO dto = new EventLogDTO();
                        dto.Loglevel = (LogLevel)dr.GetInt16(0);
                        dto.Event_Entry = dr.GetString(1);
                        dto.Username= dr.GetString(2);
                        dto.Date = dr.GetDateTime(3);
                        eventLogDTO.Add(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while trying to show the EventLog");
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
            return eventLogDTO;
        }


        // sql Error handling
        private void ErrorCatch(Exception ex)
        {
            conn.Close();
            EventLogSubViewModel ELog = new();
            AddEventLog(ELog.LogEvent(ex.Message, LogLevel.Error, GetIdFromUsername()));
            MessageBox.Show($"Unexpected Error occured: {ex.Message}");
        }



        public List<string> GetPlateList()
        {
            List<string> PlateList = new List<string>();
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT Plate FROM Vehicle", conn); 
                    dr = cmnd.ExecuteReader();
                    while (dr.Read()) 
                    { 
                        PlateList.Add(dr.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
            return PlateList;
        }

        public void CreateDriveLog(DriveLogDTO AddDTO)
        {
            try
            {
                conn.Open();
                {
                    cmnd = new("INSERT INTO DRIVELOG VALUES(@ADDUSERNAME, @ADDPLATE, @ADDDATE, @ADDSTARTTIME, @ADDENDTIME, @ADDDISTANCE)",conn);
                    cmnd.Parameters.AddWithValue("@ADDUSERNAME",AddDTO.Username);
                    cmnd.Parameters.AddWithValue("@ADDPLATE",AddDTO.Plate);
                    cmnd.Parameters.AddWithValue("@ADDDATE",AddDTO.Date);
                    cmnd.Parameters.AddWithValue("@ADDSTARTTIME",AddDTO.StartTime);
                    cmnd.Parameters.AddWithValue("@ADDENDTIME",AddDTO.EndTime);
                    cmnd.Parameters.AddWithValue("@ADDDISTANCE",AddDTO.Distance);

                    cmnd.ExecuteNonQuery();
                    MessageBox.Show("KørselsLog Oprettet!");
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                cmnd.Dispose();
                conn.Close();
            }

        }

        internal List<string> GetUsernameList()
        {
            List<string> names = new();
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT Username FROM [USER]", conn);
                    dr = cmnd.ExecuteReader();
                    while (dr.Read())
                    {
                        names.Add(dr.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally 
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
            return names;
        }

        internal List<DriveLogDTO> GetDriveLog(string SelectedUser)
        {
            List<DriveLogDTO> data = new();
            try
            {
                conn.Open();
                {
                    cmnd = new("SELECT Username, Plate, Date, StartTime, EndTime, Distance FROM DriveLog WHERE Username = @user", conn);
                    cmnd.Parameters.AddWithValue("@user", SelectedUser);

                    dr = cmnd.ExecuteReader();
                    while (dr.Read()) 
                    {
                        DriveLogDTO dto = new();

                        dto.Username = dr.GetString(0);
                        dto.Plate = dr.GetString(1);

                        // Convert the datetime to HH:mm format

                        dto.Date = dr.GetDateTime(2);
                        dto.StartTime = dr.GetDateTime(3);
                        dto.EndTime = dr.GetDateTime(4);



                        dto.Distance = dr.GetInt32(5);

                        data.Add(dto);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
            finally
            {
                dr.Close();
                cmnd.Dispose();
                conn.Close();
            }
            return data;
        }

        // This should be the way to use sqlconnection instead of the way I do it now
        // This closes the connection without having to explicitly type conn.close();
        private void test()
        {
            try
            {
                //private SqlConnection? conn = new(sqlconn);
                using (SqlConnection? conn = new(sqlconn))
                {
                    // not sure if I need to open connection here but I probably do
                    using (SqlDataAdapter da = new())
                    {
                        // A lot of nesting
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorCatch(ex);
            }
        }

    }
}
