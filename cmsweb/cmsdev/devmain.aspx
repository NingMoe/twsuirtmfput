<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DevMain" CodeFile="DevMain.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE 
id=onetidTitle>系统工具－主页</TITLE>
		<meta http-equiv="Pragma" content="no-cache" >
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" method="post" runat="server">
			<TABLE  width="100%" border="0">
				<TBODY>
					<TR>
						<TD width="400" valign="top">
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="400" border="0">
								<TR>
									<TD class="header_level2" align="center" height="19">系统工具</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="36"><asp:linkbutton id="lbtnExit" runat="server" ForeColor="Red">退出工具系统</asp:linkbutton></TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnAppConfig2" runat="server">系统配置信息</asp:linkbutton>
									</TD> 
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnSwitch" runat="server">系统功能开关</asp:linkbutton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:LinkButton id="lbtnDebug" runat="server">系统调试</asp:LinkButton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnSysConfig" runat="server">系统手动高级配置</asp:linkbutton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnGetLog" runat="server">提取技术日志</asp:linkbutton>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnUpdateDb" runat="server">更新SQL数据库</asp:linkbutton>
										<asp:CheckBox id="chkCheckLastUpdateTime" runat="server" Text="从最近更新日期开始更新（否则从头开始更新）" Checked="True"></asp:CheckBox>
									</TD>
								</TR>
								<TR align="center" height="30" style="DISPLAY:none">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnUpdateMdb" runat="server">更新MDB数据库</asp:linkbutton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnGetUpdateTime" runat="server">提取最新更新时间</asp:linkbutton>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="26">
										<asp:linkbutton id="Linkbutton1" runat="server" OnClick="ExchangeDocumentCenter">[新功能更新]文档能够存放在多个数据库中</asp:linkbutton>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="26">
										<span style="color:Red;">*</span><a href="../UpdatePwd.aspx" target="_blank" > 更新密码加密方式</a>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<td valign="top">
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0"  border="0" width="100%">
								<TR>
									<TD class="header_level2" align="center" height="19">系统信息</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="28"><asp:textbox id="txtSysError" runat="server" Height="228px" Width="610" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="28"><asp:linkbutton id="lbtnClearNotes" runat="server">清空系统信息</asp:linkbutton></TD>
								</TR>
							</TABLE>
						</td>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
