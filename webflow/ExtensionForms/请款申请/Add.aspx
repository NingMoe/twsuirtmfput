<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="NetReusables" %>
<%@ Import Namespace="Unionsoft.Workflow.Items" %>
<%@ Import Namespace="Unionsoft.Workflow.Platform" %>
<%@ Import Namespace="Unionsoft.Workflow.Engine" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase"
    ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>请款申请</title>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script language="vb" runat="server">
        Private C3_282995029781 As String '物品名称
        Private C3_282995087546 As String '数量
        Private C3_282995098390 As String '单价
        Private C3_282995108593 As String '用途
        Private C3_282995628468 As String '金额
        Private IsHidden As String="block"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim RecID As String = Request("RecID")
            If RecID <> "" Then
                Dim strSql As String = "SELECT * FROM CT282994832750 where Id=" & RecID
                Dim dt As DataTable = SDbStatement.Query(strSql).Tables(0)
                If dt.Rows.Count = 1 Then
                    C3_282995029781 = dt.Rows(0)("C3_282995029781").ToString()
                    C3_282995087546 = dt.Rows(0)("C3_282995087546").ToString()
                    C3_282995098390 = dt.Rows(0)("C3_282995098390").ToString()
                    C3_282995108593 = dt.Rows(0)("C3_282995108593").ToString()
                    C3_282995628468 = dt.Rows(0)("C3_282995628468").ToString()
                    IsHidden="none"
                End If
            End If
            If Not Me.IsPostBack Then Return
        End Sub
    </script>
    <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/icon.css"/> 
    <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../easyUI/easyui-lang-zh_CN.js" ></script>
    <script language="javascript" type="text/javascript">
 
    function fnParentSave() {
        var C3_282995029781 = $("#C3_282995029781").val(); //物品名称
        var C3_282995087546 = $("#C3_282995087546").val(); //数量
        var C3_282995098390 = $("#C3_282995098390").val(); //单价
        var C3_282995108593 = $("#C3_282995108593").val(); //用途
        var C3_282995628468 = $("#C3_282995628468").val(); //金额
        window.parent.$('#CenterGrid').datagrid('appendRow', {
            物品名称: C3_282995029781,
            数量: C3_282995087546,
            单价: C3_282995098390,
            用途: C3_282995108593,
            金额: C3_282995628468
        });
        window.parent.ParentCloseWindow();
       
    }
    //验证
    function Validate()
    {
        if(!CheckValue(Form1)) return false;
        return true;
    }
    </script>
    <form id="Form1" method="post" runat="server">
        <h1 align="center">请款申请</h1>
        <table width="500" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
             <tr>
                <td width="100" class="F_center">
                    物品名称</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282995029781" type="text" value="<%=C3_282995029781 %>" class="noborder" id="C3_282995029781" style="width:95%" /></td>
            </tr>
            <tr>
                <td class="F_center" >数量</td>
                <td align="left"><input name="C3_282995087546" type="text"  class="noborder" value="<%=C3_282995087546 %>" id="C3_282995087546" style="width: 95%" /></td>
            </tr>
          <tr>
                <td class="F_center">
                    单价</td>
                <td align="left" valign="top">
                    <input name="C3_282995098390"  type="text" class="noborder" id="C3_282995098390" value="<%=C3_282995098390 %>"  style="width: 95%;" /></td>
            </tr>
            <tr>
                <td class="F_center"  style="height:40px;">用途</td>  
                <td align="left"   valign="center" >
                    <textarea name="C3_282995108593"  type="text" class="noborder" id="C3_282995108593"  style="width: 95%;height:60px;" ><%=C3_282995108593 %></textarea>
                </td>  
            </tr>
            <tr> 
                <td class="F_center" style="height: 40px;">金额 </td>
                <td align="left"  valign="center" >
                     <input name="C3_282995628468" id="C3_282995628468" value="<%=C3_282995628468 %>"    class="noborder" type="text" style="width: 95%;" />
                </td>
            </tr>
            <tr><td align="right" colspan="2" ><input type="image" style="display:<%=IsHidden %>"  id="btnParentSave" src="../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnParentSave(); return false;" /></td></tr>
        </table>
    </form>
</body>
</html>

