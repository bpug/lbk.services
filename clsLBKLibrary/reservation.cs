using System;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using clsLBKLibrary.loewenbraeusms;


namespace clsLBKLibrary
{
    public class Reservation
    {
        private Guid _guidReservationId = System.Guid.NewGuid();
        private DateTime _dateReservationTime = System.DateTime.Now.AddYears(-10);
        private string _strFingerprint = "";
        private string _strMobile = "";
        private string _strGuestName = "";
        private int _intSeats = 0;
        private string _strAdvice = "";
        private string _strConfirmCode = "";

        public enum StatusArt
        {
            None = 0,
            Requested = 1,
            ConfirmedByRestaurant = 2,
            ConfirmedByCustomer = 3,
            DeclinedByRestaurant = 4,
            DeclinedAfterConfirmedByRestaurant = 5,
            AbortedByCustomer = 6
        }

        private StatusArt _intStatus = 0;
        private string _strDeclineReason = "";
        private DateTime _dateLastChange = System.DateTime.Now;


        public Guid ReservationId
        {
            get { return _guidReservationId; }
            set { _guidReservationId = value; }
        }

        public DateTime ReservationTime
        {
            get { return _dateReservationTime; }
            set { _dateReservationTime = value; }
        }

        public string Fingerprint
        {
            get { return _strFingerprint; }
            set { _strFingerprint = value; }
        }

        public string Mobile
        {
            get { return _strMobile; }
            set { _strMobile = value; }
        }

        public string GuestName
        {
            get { return _strGuestName; }
            set { _strGuestName = value; }
        }

        public int Seats
        {
            get { return _intSeats; }
            set { _intSeats = value; }
        }

        public string Advice
        {
            get { return _strAdvice; }
            set { _strAdvice = value; }
        }

        public string ConfirmCode
        {
            get { return _strConfirmCode; }
            set { _strConfirmCode = value; }
        }

        public StatusArt Status
        {
            get { return _intStatus; }
            set { _intStatus = value; }
        }

        public string DeclineReason
        {
            get { return _strDeclineReason; }
            set { _strDeclineReason = value; }
        }

        public DateTime LastChange
        {
            get { return _dateLastChange; }
            set { _dateLastChange = value; }
        }

        public Reservation()
        {
        }

        public Reservation(Guid reservationId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"SELECT * from Reservation where ReservationId='" + reservationId + "'";
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand(strSql, objConn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    _guidReservationId = reservationId;
                    //_dateReservationTime = DateTime.Parse(myReader["reservationtime"].ToString());
                    _dateReservationTime = (DateTime) myReader["reservationtime"];
                    _strFingerprint = myReader["fingerprint"].ToString();
                    _strMobile = myReader["mobile"].ToString();
                    _strGuestName = myReader["guestname"].ToString();
                    _intSeats = int.Parse(myReader["seats"].ToString());
                    _strAdvice = myReader["advice"].ToString();
                    _strConfirmCode = myReader["confirmcode"].ToString();
                    _intStatus = (StatusArt) int.Parse(myReader["status"].ToString());
                    _strDeclineReason = myReader["declinereason"].ToString();
                    _dateLastChange = DateTime.Parse(myReader["lastchange"].ToString());
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                objConn.Close();
                Logger.Append("ReservationList: " + ex.Message, Logger.ERROR);
            }
            finally
            {
                objConn.Close();
            }
        }

        public void Add()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                var 
                strSql =
                    @"INSERT INTO [Reservation]
           ([ReservationId]
           ,[ReservationTime]
           ,[Fingerprint]
           ,[Mobile]
           ,[GuestName]
           ,[Seats]
           ,[Advice]
           ,[ConfirmCode]
           ,[Status])
     VALUES
           (@ReservationId
           ,@ReservationTime
           ,@Fingerprint
           ,@Mobile
           ,@GuestName
           ,@Seats
           ,@Advice
           ,@ConfirmCode
           ,@Status)";


                    var sqlCommand = new SqlCommand(strSql, objConn);
                    sqlCommand.Parameters.Add(new SqlParameter("@ReservationId", (object)ReservationId ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@ReservationTime", (object)ReservationTime ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@Fingerprint", (object)Fingerprint ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@Mobile", (object)Mobile ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@GuestName", (object)GuestName ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@Seats", (object)Seats ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@Advice", (object)Advice ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@ConfirmCode", (object)ConfirmCode ?? (object)DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@Status", (object)"1" ?? (object)DBNull.Value));
                    //var sqlp = sqlCommand.Parameters.Cast<SqlParameter>().Aggregate("", (current, p) => current + "[" + p.ParameterName.ToString() + "]" + p.SqlValue.ToString());

                    objConn.Open();
                    sqlCommand.ExecuteScalar();
                    objConn.Close();

                }
                catch (Exception ex)
                {
                    Logger.Append("ReservationAdd: " + ex.Message, Logger.ERROR);
                    objConn.Close();
                }


            //    var zeitpunkt = _dateReservationTime.ToString("yyyy-MM-dd hh:mm:00");
            //    strSql = strSql.Replace("0000-00-00", zeitpunkt);
            //    strSql = strSql.Replace("unique", _guidReservationId.ToString());
            //    strSql = strSql.Replace("xxmobile", _strMobile);
            //    strSql = strSql.Replace("xxname", _strGuestName);
            //    strSql = strSql.Replace("xxfingerprint", _strFingerprint);
            //    strSql = strSql.Replace("xxseats", _intSeats.ToString(CultureInfo.InvariantCulture));
            //    strSql = strSql.Replace("xxadvice", _strAdvice);
            //    strSql = strSql.Replace("xxconf", _strConfirmCode);
            //    var myCommand = new SqlCommand(strSql, objConn);
            //    myCommand.ExecuteNonQuery();
            //    objConn.Close();
            //}
            //catch (Exception ex)
            //{
            //    Logger.Append("ReservationAdd: " + ex.Message, Logger.ERROR);
            //    objConn.Close();
            //}
        }

        public Guid SetConfirmation(Guid reservationId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"update Reservation set status=2 where ReservationId='" + reservationId + "'";
                var myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
                objConn.Close();
            }
            catch (Exception ex)
            {
                Logger.Append("SetConfirmation: " + ex.Message, Logger.ERROR);
                objConn.Close();
            }

            var ergebnis = new Reservation(reservationId);
            return ergebnis._guidReservationId;
        }

        public Guid SetConfirmationCustomer(Guid reservationId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"update Reservation set status=3 where ReservationId='" + reservationId + "'";
                var myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
                objConn.Close();
            }
            catch (Exception ex)
            {
                Logger.Append("SetConfirmationCustomer: " + ex.Message, Logger.ERROR);
                objConn.Close();
            }

            var ergebnis = new Reservation(reservationId);
            var sms = new Service1();
            var smstext = "Lieber Gast, wir haben fuer Sie " + ergebnis.Seats.ToString(CultureInfo.InvariantCulture) +
                          " Plaetze fuer den " + ergebnis.ReservationTime.ToString("dd.MM.yyyy") + " um " +
                          ergebnis.ReservationTime.ToString("HH:mm") + " reserviert. Ihr Loewenbraeukeller Team";
            smstext = smstext.PadRight(160, ' ');
            var smsAntwort = sms.sendSMS(ergebnis.Mobile, smstext);
            return ergebnis._guidReservationId;
        }


        public Guid SetDeclining(Guid reservationId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();

                var strSql = @"update Reservation set status=4 where ReservationId='" + reservationId + "'";
                var myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
                objConn.Close();
            }
            catch (Exception ex)
            {
                Logger.Append("SetDeclining: " + ex.Message, Logger.ERROR);
                objConn.Close();
            }

            var ergebnis = new Reservation(reservationId);
            return ergebnis._guidReservationId;
        }

        public List<Reservation> GetReservationsByStatus(int status)
        {
            var lstOpenReservations = new List<Reservation>();
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;

            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"SELECT top 15 ReservationId from Reservation where status=" +
                             status.ToString(CultureInfo.InvariantCulture)+" order by lastchange desc";
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand(strSql, objConn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    lstOpenReservations.Add(new Reservation(new Guid(myReader["ReservationId"].ToString())));
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Logger.Append("GetReservationsByStatus: " + ex.Message, Logger.ERROR);
                objConn.Close();
            }

            objConn.Close();
            return lstOpenReservations;
        }

        public Boolean IsDeclinedByRestaurant(Guid reservationId)
        {
            var ergebnis = new Reservation(reservationId);
            return (ergebnis.Status == StatusArt.DeclinedByRestaurant);
        }
    }
}
