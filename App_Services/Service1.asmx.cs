using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using clsLBKLibrary;


namespace App_Services
{
    using System.IO;

    /// <summary>
    /// Zusammenfassungsbeschreibung für Service1
    /// </summary>
    [WebService(Namespace = "http://loewenbraeuservice.ip-connect.de/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Um das Aufrufen dieses Webdiensts aus einem Skript mit ASP.NET AJAX zuzulassen, heben Sie die Auskommentierung der folgenden Zeile auf. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public DishesOfTheDay TodaysMenu(string requestedDate, string fingerprint)
        {
            var dishesOfTheDay = new DishesOfTheDay();
            DateTime today;
            try
            {
                today = DateTime.Parse(requestedDate);
            }
            catch (Exception ex) {
                var fehler = new category {Title = "Es ist ein Fehler aufgetreten", Subtitle = ex.Message};
                dishesOfTheDay.DishOfTheDay.Add(fehler);
                return dishesOfTheDay;
            }

            var lastCategory="";

            var sConnectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;

            var objConn = new SqlConnection(sConnectionString);
            objConn.Open();

            var strSql = @"SELECT dbo.Categories.Title AS CatTitle, dbo.Categories.Description AS CatDescription, dbo.Foods.Title AS DishTitle, 
                      dbo.Foods.Description AS DishDescription, dbo.Foods.Price AS DishPrice
                      FROM dbo.Foods INNER JOIN
                      dbo.Categories ON dbo.Foods.CategoryId = dbo.Categories.Id INNER JOIN
                      dbo.Menus ON dbo.Foods.MenuId = dbo.Menus.Id
                      WHERE     (dbo.Menus.Date = CONVERT(DATETIME, '0000-00-00 00:00:00', 102))
                      ORDER BY dbo.Foods.SortOrder";

            if (fingerprint!="0")
            {
                var l = new Log();
                l.Add(fingerprint, 1);
            }
            
            var zeitpunkt = today.ToString("yyyy-MM-dd");
            strSql = strSql.Replace("0000-00-00", zeitpunkt);
            SqlDataReader myReader = null;
            var myCommand = new SqlCommand(strSql, objConn);
            myReader = myCommand.ExecuteReader();
            var tmpCategory = new category[20];
            var catNr = -1;
            while (myReader.Read())
            {
                var catTitle = myReader["CatTitle"].ToString();
                var catDescription = myReader["CatDescription"].ToString();
                var dishTitle = myReader["DishTitle"].ToString();
                var dishDescription = myReader["DishDescription"].ToString();
                var dishPrice = myReader["DishPrice"].ToString();

                if (lastCategory != catTitle)
                    catNr = catNr + 1;

                var x = new category {Title = catTitle, Subtitle = catDescription};

                if (lastCategory != catTitle)
                    tmpCategory[catNr] = x;

                lastCategory = catTitle;

                var tmpDish = new dish {Headline = dishTitle, Description = dishDescription, Price = dishPrice};

                tmpCategory[catNr].Dishes.Add(tmpDish);
            }
            myReader.Close();
            for (var i = 0; i <= catNr; i++)
            {
            dishesOfTheDay.DishOfTheDay.Add(tmpCategory[i]);
            }
            objConn.Close();
            return dishesOfTheDay;
        }

        [WebMethod]
        public string CreateReservation(string when,int seats, string mobile, string fingerprint, string name, string advice, string confirmcode )
        {
            var ergebnis = "";
            DateTime today;
            try
            {
                today = DateTime.Parse(when);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            var sConnectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(sConnectionString);
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

                var id = Guid.NewGuid().ToString();
                var sqlCommand = new SqlCommand(strSql, objConn);
                sqlCommand.Parameters.Add(new SqlParameter("@ReservationId", (object)id ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@ReservationTime", (object)when ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@Fingerprint", (object)fingerprint ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@Mobile", (object)mobile ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@GuestName", (object)name ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@Seats", (object)seats ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@Advice", (object)advice ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@ConfirmCode", (object)confirmcode ?? (object)DBNull.Value));
                sqlCommand.Parameters.Add(new SqlParameter("@Status", (object)"1" ?? (object)DBNull.Value));
                //var sqlp = sqlCommand.Parameters.Cast<SqlParameter>().Aggregate("", (current, p) => current + "[" + p.ParameterName.ToString() + "]" + p.SqlValue.ToString());

                objConn.Open();
                sqlCommand.ExecuteScalar();
                objConn.Close();
                ergebnis = id;
            }
            catch (Exception ex)
            {
                Logger.Append("ReservationAdd: " + ex.Message, Logger.ERROR);
                objConn.Close();
            }

           
            return ergebnis;
        }

        [WebMethod]
        public Reservation GetReservationById(Guid reservationId)
        {
            var ergebnis = new Reservation(reservationId);
            return ergebnis;
        }

        [WebMethod]
        public Guid CreateReservationByObject(Reservation tmpReservation)
        {
            tmpReservation.Add();
            return tmpReservation.ReservationId;
        }

        [WebMethod]
        public Boolean SetReservationConfirm(Guid reservationId)
        {
            var ergebnis = new Reservation().SetConfirmation(reservationId);
            return ergebnis == reservationId;
        }

        [WebMethod]
        public Boolean IsDeclinedByRestaurant(Guid reservationId)
        {
            return  new Reservation().IsDeclinedByRestaurant(reservationId);
        }

        [WebMethod]
        public Boolean SetReservationConfirmCustomer(Guid reservationId)
        {
            var ergebnis = new Reservation().SetConfirmationCustomer(reservationId);
            return ergebnis == reservationId;
        }


        [WebMethod]
        public Boolean SetDeclining(Guid reservationId)
        {
            var ergebnis = new Reservation().SetDeclining(reservationId);
            return ergebnis == reservationId;
        }

        [WebMethod]
        public List<Reservation> GetReservationsByStatus(int status) 
        {
            var tmpErgebnis = new Reservation();
            var ergebnis = tmpErgebnis.GetReservationsByStatus(status);
            return ergebnis;
        }

        [WebMethod]
        public List<Event> GetEvents(string fingerprint)
        {
            if (fingerprint != "0")
            {
                var l = new Log();
                l.Add(fingerprint, 3);
            }
            var tmpErgebnis = new Events();
            return tmpErgebnis.GetEvents();
        }

        [WebMethod]
        public List<Picture> GetPictures(string fingerprint)
        {
            if (fingerprint != "0")
            {
                var l = new Log();
                l.Add(fingerprint, 6);
            }
            var tmpErgebnis = new Pictures();
            return tmpErgebnis.GetPictures();
        }

        [WebMethod]
        public List<Video> GetVideos(string fingerprint)
        {
            if (fingerprint != "0")
            {
                var l = new Log();
                l.Add(fingerprint, 7);
            }
            var tmpErgebnis = new Videos();
            return tmpErgebnis.GetVideos();
        }

        [WebMethod]
        public Question GetQuestion(int id)
        {
            var tmpErgebnis = new Question(id);
            return tmpErgebnis;
        }

        [WebMethod]
        public Quiz GetQuiz(string fingerprint, int anzahlFragen)
        {
            if (fingerprint != "0")
            {
                var l = new Log();
                l.Add(fingerprint, 2);
            }
            var tmpErgebnis = new Quiz(anzahlFragen);
            return tmpErgebnis;
        }

        [WebMethod]
        public bool ActivateVoucher(string fingerprint, int quizId, string code )
        {
            var l = new Log();
            l.Add(fingerprint, 8);
            var tmpErgebnis = new Voucher().Activate(fingerprint,quizId,code);
            return tmpErgebnis;
        }
        [WebMethod]
        public bool DischargeVoucher(string fingerprint, int quizId, string code)
        {
            var l = new Log();
            l.Add(fingerprint, 9);
            var tmpErgebnis = new Voucher().Discharge(fingerprint, quizId, code);
            return tmpErgebnis;
        }

        [WebMethod]
        public DateTime? GetMenuLastUpdate(string fingerprint)
        {
            var l = new Log();
            l.Add(fingerprint, 10);

            var path = ConfigurationManager.AppSettings["SpeisekarteServerPath"];

            if (!File.Exists(path))
            {
                return null;
            }

            return File.GetLastWriteTime(path);
        }
    }
}
