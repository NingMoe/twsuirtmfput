<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysDbUpdate" CodeFile="SysDbUpdate.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ϵͳ���ݿ����</title>
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
								<TD colspan="2" class="form_header"><b><asp:Label id="lblTitle" runat="server">���ݿ�����</asp:Label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 80px" align="right" width="80"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 80px; HEIGHT: 24px" align="right" width="80"></TD>
								<TD style="HEIGHT: 24px"><FONT face="����"><asp:label id="Label4" runat="server">���ݿ�汾���²����Ƚϼ򵥣�ֻ������ť"��ʼ�Զ�����"������������ݿ�ϵͳ����Ҫ�Ľṹ�ԸĶ������������������̻�ʵʩ�̵ļ���֧���²����ֶ����¡��ֶ����²������������ʱ�䡣</asp:label><BR>
										<BR>
										<asp:label id="lblDbInfo" runat="server"></asp:label><BR>
										<asp:label id="lblDbVersion" runat="server"></asp:label><BR>
										<asp:label id="lblManualUpdateTime" runat="server"></asp:label><BR>
										<asp:label id="lblLastOperateTime" runat="server"></asp:label></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 80px; HEIGHT: 21px" align="right" width="80"></TD>
								<TD style="HEIGHT: 21px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 80px; HEIGHT: 17px" align="right" width="80"></TD>
								<TD style="HEIGHT: 17px"><asp:button id="btnUpdateFromFile" runat="server" Text="��ʼ�Զ�����" Width="140px"></asp:button>
									<asp:button id="btnUpdateMdb" runat="server" Width="140px" Text="����MDB�ļ�"></asp:button><asp:CheckBox id="chkCheckLastUpdateTime" runat="server" Text="������������ڿ�ʼ���£������ͷ��ʼ���£�" Checked="True"></asp:CheckBox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 80px; HEIGHT: 8px" align="right" valign="top" width="80"><asp:linkbutton id="lbtnClearResult" runat="server">��ո��½��</asp:linkbutton></TD>
								<TD style="HEIGHT: 8px"><FONT face="����"><asp:textbox id="txtResult" runat="server" Width="100%" Height="180px" TextMode="MultiLine" ReadOnly="True"></asp:textbox></FONT></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 80px" align="right" width="80"></TD>
								<TD align="left"><asp:button id="btnUpdateDbFromSql" runat="server" Text="��ʼ�ֶ�ִ��SQL" Width="140px"></asp:button><FONT face="����"><asp:button id="btnReadAll" runat="server" Text="��ȡ���и�����Ϣ" Width="140px"></asp:button></FONT><asp:button id="btnGetManual" runat="server" Text="��ȡ�ֶ�������Ϣ" Width="140px"></asp:button></TD>
							</TR>
							<TR height="25">
								<TD style="WIDTH: 80px" vAlign="top" align="right" width="80"><FONT face="����"><asp:linkbutton id="lbtnClearSql" runat="server">���SQL</asp:linkbutton></FONT></TD>
								<TD><asp:textbox id="txtSql" runat="server" Width="100%" Height="180px" TextMode="MultiLine"></asp:textbox></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
