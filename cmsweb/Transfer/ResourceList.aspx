<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResourceList.aspx.vb" Inherits="Transfer_ResourceList" %>
 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
		<LINK href="../css/cmsstyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:TextBox ID="txtRecID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="txtResID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="TextToFileNum" runat="server" style="display:none"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="确定" />
    <div>
        <asp:DataGrid ID="dgResource" runat="server"> 
        </asp:DataGrid>
         
    </div>
    </form>
</body>
</html>

<script language="javascript">
function selectedRow(obj)
{
    var trList=document.getElementById("dgResource").getElementsByTagName("tr") ;
    for(var i=1;i<trList.length;i++)
    {
        trList[i].bgColor="#ffffff";
    }
    obj.bgColor = "#C4D9F9";
    document.getElementById("txtRecID").value=obj.Recid;
    document.getElementById("txtResID").value=obj.Resid;
    document.getElementById("TextToFileNum").value=obj.FileNum;
}
</script>
