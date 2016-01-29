<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ReservationWEB.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script type="text/JavaScript">
<!--
        function timedRefresh(timeoutPeriod) {
            setTimeout("location.reload(true);", timeoutPeriod);
        }
//   -->
</script>
<title>Offene Reservierungen</title>    
</head>
<body onload="JavaScript:timedRefresh(120000);">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

        <br />
    Unbearbeitete Reservierungsanfragen:
    <asp:GridView ID="GridViewReservationList1" runat="server"  
            AutoGenerateColumns="False"  BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" 
            GridLines="Both" >
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
         <Columns >
            <asp:BoundField DataField="reservationtime" HeaderText="Wann" DataFormatString="{0:dd.MM.yyyy HH:mm}"  />
            <asp:BoundField DataField="guestname" HeaderText="Gast" />
            <asp:BoundField DataField="seats" HeaderText="Plätze" />
            <asp:BoundField DataField="mobile" HeaderText="Telefon" />  
            <asp:BoundField DataField="advice" HeaderText="Hinweis" /> 
            <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ReservationId","zusagen.aspx?ReservationId={0}" ) %>'
                        Text="zusagen"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ReservationId","absagen.aspx?ReservationId={0}" ) %>'
                        Text="ablehnen"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>        
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />       
        </asp:GridView>
<br />
Unbestätigte Reservierungsanfragen
    <asp:GridView ID="GridViewReservationList2" runat="server"  
            AutoGenerateColumns="False"  BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" 
            GridLines="Both" >
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
         <Columns >
            <asp:BoundField DataField="reservationtime" HeaderText="Wann" DataFormatString="{0:dd.MM.yyyy HH:mm}"  />
            <asp:BoundField DataField="guestname" HeaderText="Gast" />
            <asp:BoundField DataField="seats" HeaderText="Plätze" />
            <asp:BoundField DataField="mobile" HeaderText="Telefon" />  
            <asp:BoundField DataField="advice" HeaderText="Hinweis" />           
        </Columns>        
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />       
        </asp:GridView>
        
<br />
Vom Gast bestätigte Anfragen
    <asp:GridView ID="GridViewReservationList3" runat="server"  
            AutoGenerateColumns="False"  BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" 
            GridLines="Both" >
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
         <Columns >
            <asp:BoundField DataField="reservationtime" HeaderText="Wann" DataFormatString="{0:dd.MM.yyyy HH:mm}"  />
            <asp:BoundField DataField="guestname" HeaderText="Gast" />
            <asp:BoundField DataField="seats" HeaderText="Plätze" />
            <asp:BoundField DataField="mobile" HeaderText="Telefon" />  
            <asp:BoundField DataField="advice" HeaderText="Hinweis" />           
        </Columns>        
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />       
        </asp:GridView>
        
        
        
<br />
Abgelehne Reservierung nach Freigabe
    <asp:GridView ID="GridViewReservationList5" runat="server"  
            AutoGenerateColumns="False"  BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" 
            GridLines="Both" >
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
         <Columns >
            <asp:BoundField DataField="reservationtime" HeaderText="Wann" DataFormatString="{0:dd.MM.yyyy hh:mm}"  />
            <asp:BoundField DataField="guestname" HeaderText="Gast" />
            <asp:BoundField DataField="seats" HeaderText="Plätze" />
            <asp:BoundField DataField="mobile" HeaderText="Telefon" />  
            <asp:BoundField DataField="advice" HeaderText="Hinweis" />           
        </Columns>        
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />       
        </asp:GridView>
    </form>
</body>
</html>
