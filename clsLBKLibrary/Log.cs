using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System;

namespace clsLBKLibrary
{
    public class Log
    {
        string _strFingerprint = "";

        public Log()
        {
            LogType = 0;
        }
        
        public int LogType { get; set; }
        public string Fingerprint
        {
            get { return _strFingerprint; }
            set { _strFingerprint = value; }
        }

        public void Add(string fingerprint, int logtype)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"insert into Log(fingerprint, logtype) values('xxfingerprint',xxLogType)";
                strSql = strSql.Replace("xxfingerprint", fingerprint);
                strSql = strSql.Replace("xxLogType", logtype.ToString(CultureInfo.InvariantCulture));

                var myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                
            }
            catch (Exception ex)
            {
                Logger.Append(ex.Message, Logger.ALL);
            }
            finally 
            {
                objConn.Close();
                objConn.Dispose();
            }
        }

    }
}
