<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysDbMaintain" CodeFile="SysDbMaintain.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>���ݿ�ά��</title>
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
								<TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">ϵͳά��</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:LinkButton id="lbtnCheckColumn" runat="server">ɾ�������ֶ�</asp:LinkButton><FONT face="����">&nbsp;</FONT></TD>
								<TD align="left"><FONT face="����">&nbsp;</FONT><asp:Label id="Label9" runat="server">��ϵͳ����ɾ���Ѿ���������Դ�������ֶζ�����Ϣ������ǰ���������������ݿ⡣</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 4px" align="right"></TD>
								<TD align="left" height="4"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px" align="right">
									<asp:LinkButton id="lbtnCheckDoc" runat="server">ɾ�������ĵ�</asp:LinkButton><FONT face="����">&nbsp;</FONT></TD>
								<TD align="left"><FONT face="����">&nbsp;</FONT><asp:Label id="Label1" runat="server">��ϵͳ����ɾ���Ѿ���������Դ�������ĵ�������ǰ���������������ݿ⡣</asp:Label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 140px; HEIGHT: 12px" align="right"></TD>
								<TD align="left" height="12"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
