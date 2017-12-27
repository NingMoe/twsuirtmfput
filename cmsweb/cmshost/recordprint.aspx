<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.RecordPrint" CodeFile="RecordPrint.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>打印记录</title>
		<meta http-equiv="Pragma" content="no-cache" />
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<!--media=print 这个属性可以在打印时有效-->
		<style media="print"> 
			.Noprint { DISPLAY: none } 
			.PageNext { PAGE-BREAK-AFTER: always } 
		</style>
		<style> 
			.NOPRINT { FONT-SIZE: 9pt; FONT-FAMILY: "宋体" } 
		</style>
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="/cmsweb/script/base.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="/cmsweb/script/Valid.js"></SCRIPT>
		
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE style="PADDING-LEFT: 4px; PADDING-TOP: 1px" cellSpacing="0" cellPadding="0" width="774"
				border="0">
				<tr>
					<td>
						<center class="Noprint">
							<TABLE class="toolbar_table" cellSpacing="0" border="0">
								<TR>
									<TD width="8"></TD>
									<TD noWrap align="center" width="60">
										<IMG src="/cmsweb/images/imagebuttons/save.gif" align="absMiddle" width="16" height="16"> <a href="#" onClick="document.all.WebBrowser1.ExecWB(6,6)">
											打印</a>
									</TD>
									<TD width="8" align="center">
										<IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
									</TD>
									<TD noWrap align="center" width="80">
										<IMG src="/cmsweb/images/imagebuttons/save.gif" align="absMiddle" width="16" height="16"> <a href="#" onClick="document.all.WebBrowser1.ExecWB(7,1)">
											打印预览</a>
									</TD>
									<TD width="8" align="center">
										<IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
									</TD>
									<TD noWrap align="center" width="80">
										<IMG src="/cmsweb/images/imagebuttons/save.gif" align="absMiddle" width="16" height="16"> <a href="#" onClick="document.all.WebBrowser1.ExecWB(8,1)">
											页面设置</a>
									</TD>
									<TD align="center" width="8"><IMG src="/cmsweb/images/icons/saprator.gif" align="absMiddle" width="2" height="18">
									</TD>
									<TD noWrap align="center" width="60"><IMG src="/cmsweb/images/imagebuttons/exit.gif" align="absMiddle" width="16" height="16">
										<asp:linkbutton id="lnkExit" Runat="server">退出</asp:linkbutton></TD>
									<TD align="right">&nbsp;
										<asp:Label id="lblHeader" runat="server" Width="100px"></asp:Label></TD>
								</TR>
							</TABLE>
						</center>
					</td>
				</tr>
				<tr>
					<td>
						<center class="Noprint">
						</center>
					</td>
				</tr>
			</TABLE>
		</form>
<OBJECT id=WebBrowser1 classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 width=0 > 
</OBJECT> 
		<!--
页面打印所用的控件：
<OBJECT id=WebBrowser1 classid=CLSID:8856F961-340A-11D0-A96B-00C04FD705A2 height=0 width=0 VIEWASTEXT> 
</OBJECT> 
-->
	</body>
</HTML>
