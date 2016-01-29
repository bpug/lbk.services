using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace clsLBKLibrary
{
    public class  Answers
    {
        public List<Answer> GetAnswers(int questionId)
        {
            var lstAnswers = new List<Answer>();
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            var objConn = new SqlConnection(connectionString);

            try
            {    
                objConn.Open();

                var strSql = @"SELECT * from Answers where QuestionId=" + questionId.ToString(CultureInfo.InvariantCulture);
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand(strSql, objConn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var answer = new Answer
                    {
                        Id = int.Parse(myReader["Id"].ToString()),
                        Description = myReader["Description"].ToString(),
                        Explanation = myReader["Explanation"].ToString(),
                        IsRight = bool.Parse(myReader["IsRight"].ToString()),
                        QuestionId = int.Parse(myReader["QuestionId"].ToString())
                    };
                    lstAnswers.Add(answer);
                }
                myReader.Close();
                
            }
            catch(Exception ex)
            {
                Logger.Append(ex.Message, Logger.ALL);   
            }
            finally
            {
                objConn.Close();    
            }
            
            



            //Reihenfolge der Antworten ändern
            var randNum = new Random();
            for (var i = 0; i <= 10; i++)
            {
                var tausch1 = randNum.Next(lstAnswers.Count);
                var tausch2 = randNum.Next(lstAnswers.Count);
                var pufferAntwort = lstAnswers[tausch1];
                lstAnswers[tausch1] = lstAnswers[tausch2];
                lstAnswers[tausch2] = pufferAntwort;
            }

            return lstAnswers;
        }

    }
}
