<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DevAppConfig" validateRequest="false" CodeFile="DevAppConfig.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统配置</title>
		<meta http-equiv="Pragma" content="no-cache" />
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:LinkButton id="lbtnSave" style="Z-INDEX: 101; LEFT: 28px; POSITION: absolute; TOP: 20px" runat="server">保存设置</asp:LinkButton><asp:LinkButton id="lbtnExit" style="Z-INDEX: 102; LEFT: 96px; POSITION: absolute; TOP: 20px" runat="server">退出</asp:LinkButton><asp:Panel id="Panel1" style="Z-INDEX: 103; LEFT: 24px; POSITION: absolute; TOP: 40px" runat="server"
				Width="220px" Height="180px" BorderStyle="Solid" BorderColor="Aqua" BorderWidth="1px"></asp:Panel>
			<script language="javascript">
<!--
try{
  document.all.item("SYS_DATABASE___DATABASE").focus();
}catch(exception){
}
-->
			</script>
		</form>
	</body>
</HTML>
