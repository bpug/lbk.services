using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace clsLBKLibrary
{
    public class Voucher
    {
        public int Id { get; set; }
        public DateTime InsertTime { get; set; }
        public string Fingerprint { get; set; }
        public string Code { get; set; }
        public int SeriesId { get; set; }


        public bool Activate(string fingerprint, int seriesId, string code)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql =
                    @"insert into Voucher(fingerprint,SeriesID,Code) 
                                  values('xxfingerprint'
                                  ,xxSeriesID
                                  ,'xxCode'
                                  )";
                strSql = strSql.Replace("xxfingerprint", fingerprint);
                strSql = strSql.Replace("xxSeriesID", seriesId.ToString(CultureInfo.InvariantCulture));
                strSql = strSql.Replace("xxCode", code);
                var myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Append("Voucher insert: " + ex.Message, Logger.ERROR);
                return false;
            }
            finally
            {
                objConn.Close();
            }
            return true;
        }

        public bool Discharge(string fingerprint, int seriesId, string code)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            string strSql;
            SqlCommand myCommand;
            try
            {
                objConn.Open();
                strSql = @"select * from voucher where fingerprint='xxfingerprint' and SeriesID = xxSeriesID";
                strSql = strSql.Replace("xxfingerprint", fingerprint);
                strSql = strSql.Replace("xxSeriesID", seriesId.ToString(CultureInfo.InvariantCulture));
                myCommand = new SqlCommand(strSql, objConn);
                SqlDataReader myReader = null;
                var treffer = false;

                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    treffer = true;
                    if (myReader["dischargeTime"] == DBNull.Value) continue;
                    myReader.Close();
                    objConn.Close();
                    return false;
                }
                myReader.Close();

                if (!treffer)
                {
                    objConn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                objConn.Close();
                Logger.Append("Voucher select: " + ex.Message, Logger.ERROR);
                return false;
            }
            finally
            {
                objConn.Close();
            }

            try
            {
                objConn.Open();
                strSql =
                    @"update voucher
	                                set dischargeTime = GETDATE()
	                                    where fingerprint = 'xxfingerprint' and SeriesID = xxSeriesID
                              ";

                strSql = strSql.Replace("xxfingerprint", fingerprint);
                strSql = strSql.Replace("xxSeriesID", seriesId.ToString(CultureInfo.InvariantCulture));

                myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                objConn.Close();
                Logger.Append("Voucher update discharge: " + ex.Message, Logger.ERROR);
                return false;
            }
            finally
            {
                objConn.Close();
            }
            return true;
        }
    }
}

