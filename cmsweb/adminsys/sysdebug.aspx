<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysDebug" CodeFile="SysDebug.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE id="onetidTitle">��ʾ���ź���ԴID</TITLE>
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
								<TD class="header_level2" width="320" colSpan="2" height="22"><b>ϵͳ���ܵ���</b></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Button id="btnExit" runat="server" Width="88px" Text="�˳�"></asp:Button></TD>
								<TD height="25"></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:linkbutton id="lbtnClearCache" runat="server">���ϵͳ����</asp:linkbutton>&nbsp;</TD>
								<TD height="25"><asp:label id="Label1" runat="server">�����ܽ�����ϵͳ����ʱ�ã��������ϵͳ��ǰ����Ϊ���ϵͳ���ܶ����õ����ݻ�����</asp:label></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label12" runat="server">��ʾ�ֶ��ڲ�����</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkShowColName" runat="server" Text="���ֶ���������ʾ�ֶ��ڲ�����"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label14" runat="server">��ʾ���ź���ԴID</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkShowIDForCms" runat="server" Text="�ڲ��ź���Դ���ṹ����ʾ���ź���ԴID"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label11" runat="server">���ݿ������־</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkDbSqlLog" runat="server" Text="�������ݿ��������"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label10" runat="server">����ģʽ</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkDebugMode" runat="server" Text="����ģʽ"></asp:CheckBox>&nbsp;</TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:label id="Label2" runat="server">��ȡ��Դ������Ϣ</asp:label>&nbsp;</TD>
								<TD height="25"><asp:label id="Label3" runat="server">��Դ���ƻ�ID��</asp:label><asp:textbox id="txtRes1" runat="server" Width="120px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnGetResID" runat="server">��ȡ��ԴID</asp:linkbutton>&nbsp;<asp:linkbutton id="lbtnGetResName" runat="server">��ȡ��Դ����</asp:linkbutton>&nbsp;<asp:textbox id="txtRes2" runat="server" ReadOnly="True" Width="120px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:label id="Label6" runat="server">��ȡ�ֶ���Ϣ</asp:label>&nbsp;</TD>
								<TD height="25"><asp:label id="Label4" runat="server">�ֶ��ڲ����ƣ�</asp:label><asp:textbox id="txtColName" runat="server" Width="120px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnGetColumnInfo" runat="server">��ȡ�ֶ���Ϣ</asp:linkbutton>&nbsp;
									<asp:label id="Label5" runat="server">��Դ��</asp:label><asp:dropdownlist id="ddlColRes" runat="server" Width="120px"></asp:dropdownlist><asp:textbox id="txtColDispName" runat="server" ReadOnly="True" Width="120px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:label id="Label13" runat="server">��ȡ�����ֶ�����</asp:label>&nbsp;</TD>
								<TD height="25"><asp:textbox id="txtCMNum1" runat="server" ReadOnly="True" Width="76px"></asp:textbox><asp:Label id="Label8" runat="server">��</asp:Label><asp:textbox id="txtCMNum2" runat="server" ReadOnly="True" Width="76px"></asp:textbox><asp:Label id="Label9" runat="server">��</asp:Label><asp:textbox id="txtCMNum3" runat="server" ReadOnly="True" Width="76px"></asp:textbox>&nbsp;<asp:linkbutton id="lbtnGetChongmingCol" runat="server">��ȡ����</asp:linkbutton></TD>
							</TR>
							<TR>
								<td align="right" width="135" height="4"></td>
								<TD width="200" height="4"></TD>
							</TR>
							<TR>
							<TR>
								<TD align="right" width="135" height="25"><asp:Label id="Label15" runat="server">DEMO�汾</asp:Label>&nbsp;</TD>
								<TD height="25"><asp:CheckBox id="chkIsDemoVer" runat="server" Text="��DEMO�汾"></asp:CheckBox>&nbsp;</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
