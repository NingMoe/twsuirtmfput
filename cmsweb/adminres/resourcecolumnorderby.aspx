<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceColumnOrderby" CodeFile="ResourceColumnOrderby.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>��������</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD class="form_header" colSpan="2"><b><asp:label id="lblTitle" runat="server">��������</asp:label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 25px" align="right"><asp:label id="Label8" runat="server">Ŀ����Դ</asp:label></TD>
								<TD style="HEIGHT: 25px" align="left"><asp:textbox id="txtResName" runat="server" Width="196px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 10px" align="right"><FONT face="����"><asp:label id="Label1" runat="server">�����ֶ�һ</asp:label></FONT></TD>
								<TD style="HEIGHT: 10px" align="left"><asp:dropdownlist id="ddlOrderColumn1" runat="server" Width="200px"></asp:dropdownlist><asp:dropdownlist id="ddlOrderbyType1" runat="server" Width="100px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:label id="Label2" runat="server">�����ֶζ�</asp:label></TD>
								<TD align="left"><FONT face="����"><asp:dropdownlist id="ddlOrderColumn2" runat="server" Width="200px"></asp:dropdownlist><asp:dropdownlist id="ddlOrderbyType2" runat="server" Width="100px"></asp:dropdownlist></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:label id="Label3" runat="server">�����ֶ���</asp:label></TD>
								<TD align="left"><asp:dropdownlist id="ddlOrderColumn3" runat="server" Width="200px"></asp:dropdownlist><asp:dropdownlist id="ddlOrderbyType3" runat="server" Width="100px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:label id="Label4" runat="server">�����ֶ���</asp:label></TD>
								<TD align="left"><asp:dropdownlist id="ddlOrderColumn4" runat="server" Width="200px"></asp:dropdownlist><asp:dropdownlist id="ddlOrderbyType4" runat="server" Width="100px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:label id="Label5" runat="server">�����ֶ���</asp:label></TD>
								<TD align="left"><asp:dropdownlist id="ddlOrderColumn5" runat="server" Width="200px"></asp:dropdownlist><asp:dropdownlist id="ddlOrderbyType5" runat="server" Width="100px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"><asp:label id="Label6" runat="server">�����ֶ���</asp:label></TD>
								<TD align="left"><asp:dropdownlist id="ddlOrderColumn6" runat="server" Width="200px"></asp:dropdownlist><asp:dropdownlist id="ddlOrderbyType6" runat="server" Width="100px"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right"></TD>
								<TD align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">&nbsp;</TD>
								<TD align="left"><asp:button id="btnConfirm" runat="server" Width="80px" Text="��������"></asp:button><asp:button id="btnClear" runat="server" Width="80px" Text="�������"></asp:button><asp:button id="btnExit" runat="server" Width="80px" Text="�˳�"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
