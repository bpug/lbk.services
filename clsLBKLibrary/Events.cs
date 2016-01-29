using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace clsLBKLibrary
{
    public class Events
    {
        public List<Event> GetEvents()
        {
            var lstEvents = new List<Event>();
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;

            var objConn = new SqlConnection(connectionString);
            try
            {
                
                objConn.Open();
                var strSql = @"SELECT * from Events where ActivatedAt<=GETDATE() and dateadd(d,1,ExpiresAt)>=GETDATE() and IsActivated=1 order by dateorder";
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand(strSql, objConn);
                
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var _Event = new Event
                                     {
                                         Description = myReader["Description"].ToString(),
                                         Date = myReader["Date"].ToString(),
                                         Title = myReader["Title"].ToString(),
                                         IsActivated = true,
                                         DateOrder =  DateTime.Parse(  myReader["Dateorder"].ToString()),
                                         ReservationLink = myReader["ReservationLink"].ToString(),
                                         ThumbnailLink = myReader["ThumbnailLink"].ToString()
                                         
                                     };
                    lstEvents.Add(_Event);
                }
                
                myReader.Close();
                myReader.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Append(ex.Message, Logger.ALL);
            }
            finally
            {
                Logger.Append("Events Conn close", Logger.ALL);
                objConn.Close();
                objConn.Dispose();
            }
            return lstEvents;
        }
    }
}
