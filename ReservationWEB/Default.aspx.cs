using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using clsLBKLibrary;



namespace ReservationWEB
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var resListStatus1 = new Reservation(new Guid()).GetReservationsByStatus(1);
            this.GridViewReservationList1.DataSource = resListStatus1;
            this.GridViewReservationList1.DataBind();

            var resListStatus2 = new Reservation(new Guid()).GetReservationsByStatus(2);
            this.GridViewReservationList2.DataSource = resListStatus2;
            this.GridViewReservationList2.DataBind();

            var resListStatus3 = new Reservation(new Guid()).GetReservationsByStatus(3);
            this.GridViewReservationList3.DataSource = resListStatus3;
            this.GridViewReservationList3.DataBind();        
        }
    }
}
