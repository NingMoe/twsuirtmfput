<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DocSearchFullDb" CodeFile="DocSearchFullDb.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>�����ĵ�</title>
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
								<TD class="form_header"><b><asp:label id="lblTitle" runat="server">ȫ�����</asp:label></b></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 4px" align="left"></TD>
							</TR>
							<TR>
								<TD align="left"><asp:label id="lblDocName" runat="server">�ĵ����ƣ�</asp:label><asp:textbox id="txtDocName" runat="server" Width="120px"></asp:textbox><FONT face="����">&nbsp;
									</FONT>
									<asp:label id="Label2" runat="server">��׺����</asp:label><asp:textbox id="txtDocExt" runat="server" Width="60px"></asp:textbox>&nbsp;
									<asp:label id="lblKeyword" runat="server">�ؼ��֣�</asp:label><asp:textbox id="txtDocKeyword" runat="server" Width="112px"></asp:textbox><FONT face="����">&nbsp;
									</FONT>
									<asp:label id="lblComments" runat="server">��ע��</asp:label><asp:textbox id="txtDocComments" runat="server" Width="112px"></asp:textbox>&nbsp;
									<asp:label id="Label3" runat="server">�ĵ����ݣ�</asp:label>
									<asp:textbox id="txtDocContent" runat="server" Width="196px"></asp:textbox></TD>
							<TR>
								<TD style="HEIGHT: 4px" align="left"></TD>
							</TR>
							<TR>
								<TD align="left"><asp:button id="btnConfirm" runat="server" Width="112px" Text="��ʼ����"></asp:button><FONT face="����">&nbsp;
									</FONT>
									<asp:label id="Label1" runat="server">�������������зǿ�ֵ������ģ����ѯ��</asp:label></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 4px" align="left"></TD>
							</TR>
							<TR>
								<TD align="left">
									<TABLE class="canyin_menu_table" cellSpacing="0" cellPadding="4" width="900" border="1">
										<tr bgColor="#c4d9f9">
											<td style="DISPLAY: none" width="1" height="20">&nbsp;</td>
											<td width="250" height="20">�ĵ���</td>
											<td align="right" width="40" height="20">����</td>
											<td align="right" width="70" height="20">��С</td>
											<td align="right" width="40" height="20">״̬</td>
											<td width="150" height="20">�ؼ���</td>
											<td width="100" height="20">��ע</td>
											<td width="250" height="20">������Դ�͹�����Դ(��&nbsp;|&nbsp;�ָ�)</td>
										</tr>
										<asp:repeater id="MenuRepeater" runat="server">
											<ItemTemplate>
												<tr>
													<td style="display:none" width="1px" height="20px">
														<asp:TextBox ID="txtDocID" Runat="server" style="display:none"></asp:TextBox>
													</td>
													<td width="250px" height="20px"><a href="/cmsweb/cmsdocument/DocSearchTransfer.aspx?noderesid=<%#DataBinder.Eval(Container.DataItem,"DOC2_RESID1")%>&schdocid=<%#DataBinder.Eval(Container.DataItem,"DOC2_ID")%>"><%#DataBinder.Eval(Container.DataItem,"DOC2_NAME")%></a>&nbsp;</td>
													<td width="40px" height="20px" align="right"><%#DataBinder.Eval(Container.DataItem,"DOC2_EXT")%>&nbsp;</td>
													<td width="70px" height="20px" align="right"><%#DataBinder.Eval(Container.DataItem,"DOC2_SIZE", "{0:#,##0}")%>&nbsp;</td>
													<td width="40px" height="20px" align="right"><%#DataBinder.Eval(Container.DataItem,"DOC2_STATUS")%>&nbsp;</td>
													<td width="150px" height="20px"><%#DataBinder.Eval(Container.DataItem,"DOC2_KEYWORDS")%>&nbsp;</td>
													<td width="100px" height="20px"><%#DataBinder.Eval(Container.DataItem,"DOC2_COMMENTS")%>&nbsp;</td>
													<td width="250px" height="20px"><a href="/cmsweb/cmsdocument/DocSearchTransfer.aspx?noderesid=<%#DataBinder.Eval(Container.DataItem,"DOC2_RESID1")%>"><%#DataBinder.Eval(Container.DataItem,"DOCOWNER_RESNAME_ALL")%></a>&nbsp;</td>
												</tr>
											</ItemTemplate>
										</asp:repeater></TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
