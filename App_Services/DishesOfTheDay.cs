using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App_Services
{
    public class DishesOfTheDay
    {
        List<category> _lstCategories = new List<category>();

        public List<category> DishOfTheDay
        {
            get { return _lstCategories; }
            set { _lstCategories = value; }
        }



    }
}
