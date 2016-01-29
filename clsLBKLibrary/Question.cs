using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;

namespace clsLBKLibrary
{
    public class Question
    {
        public enum QuestionCategory
        {
            None = 1,
            BAY = 2,
            BIE = 3,
            FOD = 4,
            LBK = 5,
            MUC = 6,
            SCH = 7
        }

        private QuestionCategory _initCategory = 0;

        public List<Answer> Answers { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int Points { get; set; }
        public Serie Serie { get; set; }
        public long SerieId { get; set; }

        public QuestionCategory Category
        {
            get { return _initCategory; }
            set { _initCategory = value; }
        }


        public Question()
        {
        }

        public Question(int id)
        {
            {
                Answers = new Answers().GetAnswers(id);
                var connectionString =
                    ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
                var objConn = new SqlConnection(connectionString);
                try
                {
                    objConn.Open();
                    var strSql = @"SELECT * from Questions where id=" + id.ToString(CultureInfo.InvariantCulture);
                    SqlDataReader myReader = null;
                    var myCommand = new SqlCommand(strSql, objConn);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        Description = myReader["Description"].ToString();
                        Number = int.Parse(myReader["Number"].ToString());
                        Points = int.Parse(myReader["Points"].ToString());
                        SerieId = int.Parse(myReader["SerieId"].ToString());
                        Category = (QuestionCategory) int.Parse(myReader["CategoryId"].ToString());
                    }
                    myReader.Close();
                }
                catch(Exception ex)
                {
                    objConn.Close();
                    Logger.Append("QuestionList: " + ex.Message, Logger.ERROR);
                }
                finally
                {
                    objConn.Close();
                }
            }
        }
    }
}
