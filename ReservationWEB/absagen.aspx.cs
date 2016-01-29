using System;
using clsLBKLibrary;
using clsLBKLibrary.loewenbraeusms;

namespace ReservationWEB
{
    public partial class Absagen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["ReservationId"];
            var res = new Reservation(new Guid(ID));
            var sms = new Service1();
            var smsAntwort = sms.sendSMS(res.Mobile, "Lieber Gast, leider koennen wir Ihren Reservierungswunsch nicht bestaetigen. Bitte rufen Sie uns unter  089 54726690  an. Ihr Loewenbraeukeller Team          ");
            if (smsAntwort == "OK")
                res.SetDeclining(new Guid(ID));
            Response.Redirect("default.aspx");
        }
    }
}
