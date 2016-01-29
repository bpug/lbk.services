using System;
using System.Globalization;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS_Services
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für Service1
    /// </summary>
    [WebService(Namespace = "http://loewenbraeusms.ip-connect.de/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Um das Aufrufen dieses Webdiensts aus einem Skript mit ASP.NET AJAX zuzulassen, heben Sie die Auskommentierung der folgenden Zeile auf. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {


        [WebMethod]
        public string SendSms(string mobile, string strNachricht)
        {
            var ergebnis = "OK";

            string sConnectionString;
            sConnectionString = ConfigurationManager.ConnectionStrings["lbkmobile_ConnectionString"].ConnectionString;
            //sConnectionString = Properties.Settings.Default.lbkmobile_ConnectionString;
            var objConn = new SqlConnection(sConnectionString);
            try
            {
                objConn.Open();

                var strSql = "INSERT INTO TAB_AusgangSMS (Sendezeitpunkt,Empfaenger,Nachricht,Application_ID) Values ('" + System.DateTime.Now.ToString(CultureInfo.InvariantCulture) + "','" + mobile + "','" + strNachricht + "',8)";

                var myCommand = new SqlCommand(strSql, objConn);
                myCommand.ExecuteNonQuery();
                ergebnis = "OK";
                objConn.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            

            return ergebnis;
        }
        //strSQL = "INSERT INTO TAB_AusgangSMS (Sendezeitpunkt,Empfaenger,Nachricht,Application_ID) Values ('" & Now & "','" & Handy & "','" & strNachricht & "', " & strApplicationID & ")"
    }
}
