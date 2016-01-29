using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Services
{
    public class category
    {
        string strTitle="";
        string strSubTitle="";
        List<dish> lstdishes = new List<dish>();
        public string Title 
        {
           get { return strTitle;}
           set { strTitle = value; }
        }

        public List<dish> Dishes
        {
            get { return lstdishes; }
            set { lstdishes = value; }
        }
        public string Subtitle
        {
            get { return strSubTitle; }
            set { strSubTitle = value; }
        }
    }
}
