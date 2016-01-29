using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace clsLBKLibrary
{
    public class Pictures
    {
        public List<Picture> GetPictures()
        {
            var lstPictures = new List<Picture>();
            var connectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;

            var objConn = new SqlConnection(connectionString);
            try
            {
                objConn.Open();
                var strSql = @"SELECT * from Pictures order by SortOrder";
                SqlDataReader myReader = null;
                var myCommand = new SqlCommand(strSql, objConn);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    var picture = new Picture
                                       {
                                           Description = myReader["Description"].ToString(),

                                           //return string.IsNullOrEmpty(Link) ? FilePath : Link;
                                           FileName = myReader["FileName"].ToString(),
                                           Title = myReader["Title"].ToString(),
                                           Link = myReader["Link"].ToString(),
                                           SortOrder = int.Parse(myReader["SortOrder"].ToString())
                                       };
                        
                    if (string.IsNullOrEmpty(picture.Link))
                    {
                        var path = ConfigurationManager.AppSettings["PictureHttpBasePath"];
                        picture.Link = Path.Combine(path, picture.FileName); 
                    }

                    lstPictures.Add(picture);
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                objConn.Close();
                Logger.Append("PictureList: ", Logger.ERROR);
            }
            return lstPictures;
        }
    }
}
