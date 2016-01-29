using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Services
{
    public class dish_of_the_day
    {
        List<category> lstCategories = new List<category>();

        public List<category> DishOfTheDay
        {
            get { return lstCategories; }
            set { lstCategories = value; }
        }



    }
}
