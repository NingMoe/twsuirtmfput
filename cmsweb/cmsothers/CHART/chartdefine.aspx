<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ChartDefine" CodeFile="ChartDefine.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>ChartDefine</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="/cmsweb/script/jscommon.js"></SCRIPT>
</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0" width="100%">
							<TR>
								<TD class="form_header"><b><asp:label id="lblTitle" runat="server">ͼ��ͳ������</asp:label></b></TD>
							</TR>
							<TR>
								<TD align="center">
									<table cellSpacing="1" cellPadding="0" width="80%" bgColor="#3399cc">
										<tr bgColor="white">
											<td align="right" width="20%" height="21">ͼ��ͳ�����ƣ�</td>
											<td width="30%"><asp:textbox id="txt_ChartName" runat="server" ErrorMessage="ͼ��ͳ������" mInput="true" Width="100%"></asp:textbox></td>
											<td align="right">ͳ����ʽ��</td>
											<td><asp:RadioButtonList id="rblst_ChartType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
<asp:ListItem Value="1" Selected="True">ͳ�Ƹ���</asp:ListItem>
<asp:ListItem Value="2">ͳ��ֵ</asp:ListItem>
												</asp:RadioButtonList>
											</td>
										</tr>
										<tr bgColor="white" height="21">
											<td width="20%" align="right">�����ֶ�1��</td>
											<td width="30%"><asp:dropdownlist id="ddlst_ConditionColumn1" runat="server" Width="100%"></asp:dropdownlist></td>
											<td align="right">�����ֶ�2��</td>
											<td><asp:dropdownlist id="ddlst_ConditionColumn2" runat="server" Width="100%"></asp:dropdownlist></td>
										</tr>
										<tr bgColor="white" height="21">
											<td align="right">ͳ���ֶΣ�</td>
											<td ><asp:dropdownlist id="ddlst_ValueColumn" runat="server" Width="100%"></asp:dropdownlist></td>
											<td></td><td></td>
										</tr>
										<tr bgColor="white" height="21">
											<td align="right">ͼ�����ͣ�</td>
											<td colSpan="3"><asp:RadioButtonList id="rblst_ChartKind" runat="server" RepeatDirection="Horizontal">
													<asp:ListItem Value="1" Selected="True">��״ͼ</asp:ListItem>
													<asp:ListItem Value="2">��״ͼ</asp:ListItem>
													<asp:ListItem Value="3">����ͼ</asp:ListItem>
												</asp:RadioButtonList>
											</td>
										</tr>
										<tr bgColor="white">
											<td align="right">˵����<br>
												<br>
											</td>
											<td colSpan="3"><asp:textbox id="txt_Content" runat="server" Width="100%" Rows="4" TextMode="MultiLine"></asp:textbox></td>
										</tr>
										<tr bgColor="white" height="21">
											<td align="center" colSpan="4"><asp:button id="btn_Save" runat="server" Text="����"></asp:button><asp:button id="btn_Back" runat="server" Text="����"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
