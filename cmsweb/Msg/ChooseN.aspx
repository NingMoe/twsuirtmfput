<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="NetReusables" %>
<%@ Import Namespace="Unionsoft.Platform" %>

<%@ Page Language="VB"  AutoEventWireup="false" ValidateRequest="false" %>

<!DOCTYPE html>

<script runat="server">
    Public table As DataTable
    
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strSql As String = "select ConsumerID as 客户编号,Consumer as 客户名称,Contacts as 联系人,Mobile as 手机,Phone as 座机,Address as 单位地址 from Consumer order by ID desc "
        '在此处放置初始化页的用户代码 and 创建人='" + CurrentUser.Code + "'          
        table = SDbStatement.Query(strSql).Tables(0)
    End Sub
    
     
    ''' <summary>
    ''' 查询事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
          Dim  strSql As String = "select ConsumerID as 客户编号,Consumer as 客户名称,Contacts as 联系人,Mobile as 手机,Phone as 座机,Address as 单位地址 from Consumer  "
        Dim ProjectName As String = txtProject_Name.Text.Trim
        If ProjectName <> "" Then
            strSql += " where (Consumer like '%" + ProjectName + "%' or Contacts like '%" + ProjectName + "%' or Mobile like '%" + ProjectName + "%') "
        End If
        strSql += "order by ID desc"
        table = SDbStatement.Query(strSql).Tables(0)
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <title>企业通讯录</title>
    
    <script type="text/javascript" language="javascript">
        var chooseMobile = "";
        function choose() {
scalerCheck();
            document.parentWindow.parent.document.getElementById("mobile").value = chooseMobile;
            document.parentWindow.parent.closeNotReload();
        }
        function checkedAll() {
            var chx = document.getElementById("checkAll");
            var chxary = document.getElementsByName("chkMobile");
            if (chx.checked == true) {
                for (var i = 0; i < chxary.length; i++) {
                    if (chxary[i].type = "checkbox") {
                        chxary[i].checked = true;
                        var chkId = chxary[i].id.substr(2).replace(/(^\s*)|(\s*$)/g, "");                       
                        if (chkId.length > 5) {
                            chooseMobile += chkId + ",";
                        }
                    }

                }
            }
            else {
                for (var i = 0; i < chxary.length; i++) {
                    if (chxary[i].type = "checkbox") {
                        chxary[i].checked = false;
                    }

                }
                chooseMobile = "";
            }

        }
 function scalerCheck()
        {
            var chxary = document.getElementsByName("chkMobile");          
            for (var i = 0; i < chxary.length; i++) {
                if (chxary[i].type = "checkbox") {
                    if (chxary[i].checked == true) {                       
                        var chkId = chxary[i].id.substr(2).replace(/(^\s*)|(\s*$)/g, "");
                        if (chkId.length > 5) {
                            chooseMobile += chkId + ",";
                        }
                    }

                }
            }      
        }
    </script>
</head>
<body>
   <form id="Form1" method="post" runat="server" onsubmit="">
            <table  width="700" align="center">
            
        </table>
            <table width="797" align="center" class="Bold_box" border="1" cellpadding="0" cellspacing="0" bordercolorlight="#67b2ec" bordercolordark="#ffffff">
          <tr>
              <td colspan="7">
                      <strong>客户名称 / 联系人 / 手机：</strong><asp:TextBox ID="txtProject_Name" runat="server" Width="410px"></asp:TextBox>&nbsp;<asp:Button
                        ID="Button1" runat="server" Text="查询" OnClick="Button1_Click" />&nbsp;&nbsp;&nbsp; <input type="button" id="chooseOK" onclick="choose();" value="确认选择" />
              </td>
            </tr>
            <tr align="center">
                    <td width="10%" class="left">
                    <strong>全选</strong><input type ="checkbox" id="checkAll" onclick="checkedAll();" /> </td>
                <td width="15%" class="left">
                    <strong>客户编号</strong></td>
                     <td width="20%" class="left">
                    <strong>客户名称</strong></td>
                     <td width="15%" class="left">
                    <strong>联系人</strong></td>
                     <td width="15%" class="left">
                    <strong>手机</strong></td>
                <td width="10%" class="left">
                    <strong>座机</strong></td>
                <td width="25%" class="left">
                    <strong>单位地址</strong></td>
            
            </tr>
            <%  Dim i As Int32
                For i = 0 To table.Rows.Count - 1%>
            <tr align="center" id="tr<%=i%>" style="height:25px;" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#99cc33'"
                onmouseout="this.style.background='#ffffff'">
                    <td><input type ="checkbox" id="M_<%=table.Rows(i).Item("手机").ToString & " "%>" name="chkMobile" /></td>
                    <td><%=table.Rows(i).Item("客户编号").ToString & " "%>&nbsp;</td>
                                    <td>
                    <%=table.Rows(i).Item("客户名称").ToString & " "%>
                    &nbsp;</td>
                    <td><%=table.Rows(i).Item("联系人").ToString & " "%>&nbsp;</td>
                    <td><%=table.Rows(i).Item("手机").ToString & " "%>&nbsp;</td>
                 <td><%=table.Rows(i).Item("座机").ToString & " "%>&nbsp;</td>
                    <td><%=table.Rows(i).Item("单位地址").ToString & " "%>&nbsp;</td>                    
            </tr>
            <%Next%>
        </table>
    </form>
</body>
</html>
