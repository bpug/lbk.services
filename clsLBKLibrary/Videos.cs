using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace clsLBKLibrary
{
    public class Videos
    {
        public List<Video> GetVideos()
        {
            var lstVideos = new List<Video>();
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;

            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"SELECT * from Videos order by SortOrder";
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand(strSql, objConn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var video = new Video
                                    {
                                        Description = myReader["Description"].ToString(),
                                        FileName = myReader["FileName"].ToString(),
                                        Link = myReader["Link"].ToString(),
                                        SortOrder = int.Parse(myReader["SortOrder"].ToString()),
                                        ThumbnailLink = myReader["ThumbnailLink"].ToString()
                                    };
                    lstVideos.Add(video);
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                objConn.Close();
                Logger.Append("VideosList: " + ex.Message, Logger.ERROR);
            }

            objConn.Close();
            return lstVideos;
        }
    }
}
