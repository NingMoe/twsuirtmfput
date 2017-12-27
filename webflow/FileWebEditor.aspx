<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.FileWebEditor" CodeFile="FileWebEditor.aspx.vb" CodeFileBaseClass="Unionsoft.Workflow.Web.UserPageBase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>文件在线编辑器</title>
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="css/flowstyle.css" type="text/css" rel="stylesheet">
    <SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="script/ntkoocx.js"></SCRIPT>
</HEAD>
  <body style="MARGIN:0px;OVERFLOW:auto" scroll="no">
	<form id="Form1" method="post" runat="server" action="FileWebSave.aspx">
	<OBJECT id=TANGER_OCX codeBase=officecontrol.cab#version=4,0,0,1 height="96%" width="100%" classid=clsid:C9BC4DFF-4248-4a3c-8A49-63A7D317F404 VIEWASTEXT>
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
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td align="center">
				<asp:Button id=btnUpdateFile runat="server" Text="保存文件" CssClass="ButtonCommon"></asp:Button>&nbsp;&nbsp;&nbsp;
				<INPUT type="button" VALUE="关闭窗口" class="ButtonCommon" onclick="window.close();">
				<asp:TextBox ID="txtDocumentInfo" Runat="server" style="DISPLAY:none"></asp:TextBox>
			</td>
		</tr>
	</table>
    </form>
	<script>
	TANGER_OCX_OBJ = document.all("TANGER_OCX");
	TANGER_OCX_OBJ.OpenFromURL("<%=Url%>");
	TANGER_OCX_bDocOpen=true;
	TANGER_OCX_OBJ.ActiveDocument.Application.UserName="<%=strCurrentName%>";
	<%If strDocumentExtName="doc" Then%>TANGER_OCX_OBJ.ActiveDocument.TrackRevisions = true; <%End If%>//只有word文件才能使用此属性
	//TANGER_OCX_OBJ.ActiveDocument.ReadOnly = true;
	</script>
  </body>
</HTML>

