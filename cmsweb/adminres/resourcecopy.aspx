<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceCopy" CodeFile="ResourceCopy.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>��Դ����</title>
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="490" border="0" class="table_level2">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>
										<asp:Label id="lblTitle" runat="server">������Դ</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px; HEIGHT: 4px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR height="22">
								<td width="99" align="right" style="WIDTH: 99px">
									<asp:Label id="Label1" runat="server">����Դ���ƣ�</asp:Label></td>
								<TD>
									<asp:TextBox id="txtResName" runat="server" Width="244px"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px; HEIGHT: 4px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR height="22">
								<td style="WIDTH: 99px"></td>
								<TD>
									<asp:Button id="btnConfirm" runat="server" Text="ȷ��" Width="80px"></asp:Button>
									<asp:Button id="btnCancle" runat="server" Text="�˳�" Width="80px"></asp:Button>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px; HEIGHT: 4px" align="right"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 99px" align="right" width="99"></TD>
								<TD>
									<asp:Label id="Label2" runat="server">�������������������ݣ���Դ���塢�ֶζ��塢��ʾ���á�������ơ����ƴ�����ơ�������ѡ�����ѡһѡ����Զ����롢�߼��ֵ䡢���㹫ʽ�����Ʊ��롢��Ϊ������ӱ�Ĺ������塣</asp:Label></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
