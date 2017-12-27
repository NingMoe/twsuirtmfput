<%@ Import Namespace="System.IO"%>
<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/ntkoocx.js"></SCRIPT>
<link   href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:0px;">

<script language="vb" runat="server">
Private WorkflowId As String
Private Url As String = ""
Private _Id As Long = 0

Private Sub LoadAttachment(Id As Long)
	Dim path As String
	Dim dt As DataTable
	Dim strSql As String
	path = Request.PhysicalApplicationPath & "\temp\" & Id & ".doc"
	If File.Exists(path) Then File.Delete(path)
	Dim fs As FileStream = New FileStream(path, FileMode.Create)
    Dim br As BinaryWriter = New BinaryWriter(fs)
	strSql = "SELECT FileImage FROM WORKFLOW_FORM_ATTACHMENTS WHERE ID=" & Id
	dt =  SDbStatement.Query(strSql).Tables(0)
	If dt.Rows.Count > 0 Then
		br.Write(DbField.GetObj(dt.Rows(0),"FileImage"))
	End If
	br.Close()
    fs.Close()
End Sub

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load	
	_Id = CLng(Request.QueryString("Id"))
	LoadAttachment(_Id)
	Url = "/webflow/temp/" & _Id & ".doc"
	If Not Me.IsPostBack Then Return
End Sub
</script>
<form id="Form1" method="post" runat="server">
<fieldset style="BACKGROUND: menu;width:100%">
<table cellspacing="0" cellpadding="0" height="22" width="100%" align="center">
	<tr>
		<td width=80><input type="button"  name="btnsave" value="保存" class=WorkflowNavigateButton style="width:75px;" onclick="save()"></td>
		<td width=80 align="center"><input type="button" value="关闭" name="btnclose" id="btnclose"  style="width:75px;" onclick="window.close()"/></td>
		<td width="*"></td>
	</tr>
</table>	
</fieldset>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="bold_box">
  <tr>
    <td class="f_center">
	<OBJECT id=TANGER_OCX codeBase=/webflow/officecontrol.cab#version=4,0,0,1 height="650" width="100%" classid=clsid:C9BC4DFF-4248-4a3c-8A49-63A7D317F404 VIEWASTEXT>
	<PARAM NAME="_ExtentX" VALUE="19500">
	<PARAM NAME="_ExtentY" VALUE="17727">
	<PARAM NAME="BorderColor" VALUE="14402205">
	<PARAM NAME="BackColor" VALUE="-2147483643">
	<PARAM NAME="ForeColor" VALUE="-2147483640">
	<PARAM NAME="TitlebarColor" VALUE="14402205">
	<PARAM NAME="TitlebarTextColor" VALUE="0">
	<PARAM NAME="BorderStyle" VALUE="1">
	<PARAM NAME="Titlebar" VALUE="0">
	<PARAM NAME="Toolbars" VALUE="1">
	<PARAM NAME="Caption" VALUE="Office文档在线编辑">
	<PARAM NAME="ProductCaption" VALUE="Digital Office">
	<PARAM NAME="ProductKey" VALUE="F31269C8BBCDFEF8071BB92BC3696134">
	<PARAM NAME="MakerCaption" VALUE="中国兵器工业信息中心通达科技">
	<PARAM NAME="MakerKey" VALUE="5F38E6E58308E6165BF3423D3D29548C">
	<PARAM NAME="IsShowToolMenu" VALUE="0">
	<PARAM NAME="IsNoCopy" VALUE="0">
	<PARAM NAME="IsHiddenOpenURL" VALUE="0">
	<PARAM NAME="MaxUploadSize" VALUE="0">
	<PARAM NAME="NetworkBufferSize" VALUE="0">
	<PARAM NAME="Menubar" VALUE="01">
	<PARAM NAME="Statusbar" VALUE="-1">
	<PARAM NAME="FileNew" VALUE="-1">
	<PARAM NAME="FileSave" VALUE="-1">
	<PARAM NAME="FileSaveAs" VALUE="-1">
	<PARAM NAME="FilePrint" VALUE="-1">
	<PARAM NAME="FilePrintPreview" VALUE="-1">
	<PARAM NAME="FilePageSetup" VALUE="-1">
	<PARAM NAME="FileProperties" VALUE="-1">
	<PARAM NAME="IsStrictNoCopy" VALUE="0">
	<PARAM NAME="IsUseUTF8URL" VALUE="0">
	<PARAM NAME="MenubarColor" VALUE="13160660">
	<PARAM NAME="IsUseControlAgent" VALUE="0">
	<PARAM NAME="IsUseUTF8Data" VALUE="0">
	<PARAM NAME="IsSaveDocExtention" VALUE="0">
	<PARAM NAME="IsDirectConnect" VALUE="0">
	<PARAM NAME="SignCursorType" VALUE="0">
	<PARAM NAME="IsResetToolbarsOnOpen" VALUE="0">
	<PARAM NAME="IsSaveDataIfHasVDS" VALUE="0">
	<PARAM NAME="MenuButtonStyle" VALUE="0">
	<PARAM NAME="MenuButtonColor" VALUE="16180947">
	<PARAM NAME="MenuButtonFrameColor" VALUE="14924434">
	<PARAM NAME="MenuBarStyle" VALUE="0">
	<PARAM NAME="IsGetPicOnlyOnHandSign" VALUE="0">
	<PARAM NAME="IsSecurityOptionsOpen" VALUE="0">
	<PARAM NAME="FileOpen" VALUE="-1">
	<PARAM NAME="FileClose" VALUE="-1">
	<SPAN STYLE="color:red">不能装载文档控件。请在检查浏览器的选项中检查浏览器的安全设置。</SPAN>
	</OBJECT>
	<table border="0" cellpadding="0" cellspacing="0" width="100%" style="DISPLAY:none">
		<tr>
			<td align="center">
				<asp:Button id=btnUpdateFile runat="server" Text="保存文件" CssClass="ButtonCommon"></asp:Button>&nbsp;&nbsp;&nbsp;
				<INPUT type="button" VALUE="关闭窗口" class="ButtonCommon" onclick="window.close();">
				<INPUT type="hideen" id="filename" name="filename" VALUE="<%=_Id%>.doc">
			</td>
		</tr>
	</table>
	</td>
  </tr>
</table>
</form>
<script>
TANGER_OCX_OBJ = document.all("TANGER_OCX");
TANGER_OCX_OBJ.OpenFromURL("<%=Url%>");
TANGER_OCX_bDocOpen=true;
TANGER_OCX_OBJ.ActiveDocument.Application.UserName="<%=CurrentUser.Name%>";
TANGER_OCX_OBJ.ActiveDocument.TrackRevisions = true;
//TANGER_OCX_OBJ.ActiveDocument.ReadOnly = true;

function save()
{	
	var result = TANGER_OCX_SaveEditToServerDisk("FileWebSave.aspx?action=edit&table=WF_ATTACHMENT&Id=<%=_Id%>");
	if (result==false) return false;	
	else {alert("保存成功!");return true;}
}
</script>	

</body>
</html>
