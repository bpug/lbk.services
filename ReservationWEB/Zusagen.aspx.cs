using System;
using clsLBKLibrary;
using clsLBKLibrary.loewenbraeusms;

namespace ReservationWEB
{
    public partial class Zusagen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString["ReservationId"];
            var res = new Reservation(new Guid(id));
            var sms = new Service1();
            var smsAntwort = sms.sendSMS(res.Mobile, "Lieber Gast, Ihr Loewenbraeu-Code lautet:  " + res.ConfirmCode + "  !! Erst wenn Sie diesen Code in Ihre APP eingeben ist die Reservierung verbindlich. Ihr Loewenbraeukeller Team");
            if (smsAntwort == "OK")
                res.SetConfirmation(new Guid(id));
            Response.Redirect("default.aspx");
                                
        }
    }
}
