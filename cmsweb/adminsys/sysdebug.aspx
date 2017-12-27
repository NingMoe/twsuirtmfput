<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysDebug" CodeFile="SysDebug.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">显示部门和资源ID</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<SCRIPT>
		</SCRIPT>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="740" border="0">
							<TR>
								<TD class="header_level2" width="320" colSpan="2" height="22"><b>系统功能调试</b></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Button id="btnExit" runat="server" Width="88px" Text="退出"></asp:Button></TD>
								<TD height="25"></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:linkbutton id="lbtnClearCache" runat="server">清空系统缓存</asp:linkbutton>&nbsp;</TD>
								<TD height="25"><asp:label id="Label1" runat="server">本功能仅用于系统调试时用，用于清空系统当前所有为提高系统性能而设置的数据缓存区</asp:label></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label12" runat="server">显示字段内部名称</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkShowColName" runat="server" Text="在字段设置中显示字段内部名称"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label14" runat="server">显示部门和资源ID</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkShowIDForCms" runat="server" Text="在部门和资源树结构上显示部门和资源ID"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label11" runat="server">数据库访问日志</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkDbSqlLog" runat="server" Text="生成数据库访问日期"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label10" runat="server">调试模式</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkDebugMode" runat="server" Text="调试模式"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:label id="Label2" runat="server">获取资源调试信息</asp:label>&nbsp;</TD>
								<TD height="25"><asp:label id="Label3" runat="server">资源名称或ID：</asp:label><asp:textbox id="txtRes1" runat="server" Width="120px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnGetResID" runat="server">获取资源ID</asp:linkbutton>&nbsp;<asp:linkbutton id="lbtnGetResName" runat="server">获取资源名称</asp:linkbutton>&nbsp;<asp:textbox id="txtRes2" runat="server" ReadOnly="True" Width="120px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:label id="Label6" runat="server">提取字段信息</asp:label>&nbsp;</TD>
								<TD height="25"><asp:label id="Label4" runat="server">字段内部名称：</asp:label><asp:textbox id="txtColName" runat="server" Width="120px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnGetColumnInfo" runat="server">提取字段信息</asp:linkbutton>&nbsp;
									<asp:label id="Label5" runat="server">资源：</asp:label><asp:dropdownlist id="ddlColRes" runat="server" Width="120px"></asp:dropdownlist><asp:textbox id="txtColDispName" runat="server" ReadOnly="True" Width="120px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:label id="Label13" runat="server">提取重名字段数量</asp:label>&nbsp;</TD>
								<TD height="25"><asp:textbox id="txtCMNum1" runat="server" ReadOnly="True" Width="76px"></asp:textbox><asp:Label id="Label8" runat="server">－</asp:Label><asp:textbox id="txtCMNum2" runat="server" ReadOnly="True" Width="76px"></asp:textbox><asp:Label id="Label9" runat="server">＝</asp:Label><asp:textbox id="txtCMNum3" runat="server" ReadOnly="True" Width="76px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnGetChongmingCol" runat="server">提取数量</asp:linkbutton></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label15" runat="server">DEMO版本</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkIsDemoVer" runat="server" Text="是DEMO版本"></asp:CheckBox>&nbsp;</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
