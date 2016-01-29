using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace clsLBKLibrary
{
   public class Quiz
    {
        public List<Question> Questions { get; set; }
        public int PointsProAnswer { get; set; }
        public int Id { get; set; }

       public Quiz()
       {
       }

       public Quiz(int anzahlFragen )
       {
           Questions = new List<Question>();
           //ID des aktuell gültigen Quiz ermitteln
           var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;

           var objConn = new SqlConnection(connectionString);
           objConn.Open();
           var xy = Questions.Count;

           var strSql = @"SELECT * from Series where IsActivated=1";
           SqlDataReader myReader = null;
           var myCommand = new SqlCommand(strSql, objConn);
           myReader = myCommand.ExecuteReader();
           while (myReader.Read())
           {
               Id = int.Parse(myReader["Id"].ToString());
           }
           myReader.Close();

           //Alle möglichen ID`s der Fragen ermitteln
            var fragenAuswahl= new List<int>();

           strSql = @"SELECT * from Questions where SerieID="+ Id.ToString(CultureInfo.InvariantCulture);
           myReader = null;
           myCommand = new SqlCommand(strSql, objConn);
           myReader = myCommand.ExecuteReader();
           while (myReader.Read())
           {
               fragenAuswahl.Add(int.Parse(myReader["Id"].ToString()));
           }

           myReader.Close();
           objConn.Close();

           //Reihenfolge der Fragen ändern
           var randNum = new Random();
           for (var i = 0; i <= fragenAuswahl.Count; i++)
           {
               var tausch1 = randNum.Next(fragenAuswahl.Count);
               var tausch2 = randNum.Next(fragenAuswahl.Count);
               var pufferAntwort = fragenAuswahl[tausch1];
               fragenAuswahl[tausch1] = fragenAuswahl[tausch2];
               fragenAuswahl[tausch2] = pufferAntwort;
           }

           //nur die geforderte Anzahl der Fragen anlegen
           for (var i = 0; i < anzahlFragen; i++)
               Questions.Add(new Question(fragenAuswahl[i]));
       }

    }
}
