<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportDomainUsers.aspx.vb" Inherits="adminsys_ImportDomainUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
   .Freezing 
   { 
    
   position:relative ; 
   table-layout:fixed;
   top:expression(this.offsetParent.scrollTop);   
   z-index: 10;
   } 
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="BtnOK" runat="SERVER" Text="同步" Width="80px"/>
    </div>
   <div style="overflow-y:scroll;overflow-x:auto; height: 580px;width:100%; border-width:1px; border-style:solid; border-color:#006699" id="DivGridList">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns ="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Height="580px" Width="98%" />
            <RowStyle BackColor="White" ForeColor="#003399" Font-Size="10px" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99"  />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" CssClass="Freezing" ForeColor="#CCCCFF" Font-Size="11px" />
                
                                 
                              <AlternatingRowStyle BackColor="#CCCCCC" />
        </asp:GridView>
        </div>
    </form>
    <script>
     document.getElementById("DivGridList").style.height=screen.availHeight-250;
     </script>
</body>
</html>
