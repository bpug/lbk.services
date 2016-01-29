using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Services
{
    public class dish
    {
        string strHeadline = "";
        string strDescription = "";
        string strPrice = "";

        public string Headline
        {
            get { return strHeadline; }
            set { strHeadline = value; }
        }

        public string Description
        {
            get { return strDescription; }
            set { strDescription = value; }
        }

        public string Price
        {
            get { return strPrice; }
            set { strPrice = value; }
        }
    }
}
