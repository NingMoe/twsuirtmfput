<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysLogManager" CodeFile="SysLogManager.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ϵͳ��־����</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		<!--
			function RowLeftClickNoPost(src){
				var o=src.parentNode;
				for (var k=1;k<o.children.length;k++){
					o.children[k].bgColor = "white";
				}
				src.bgColor = "#C4D9F9";
			}
		-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE class="frame_table" cellSpacing="0" cellPadding="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="4" border="0">
							<tr height="22">
								<TD style="WIDTH: 110px" align="left">��־����<BR>
									<asp:textbox id="txtLogTitle" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��Դ����<BR>
									<asp:textbox id="txtResName" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left"><FONT face="����"><asp:label id="Label1" runat="server">��־����</asp:label><BR>
										<asp:textbox id="txtLogContent" runat="server" Width="110px"></asp:textbox></FONT></TD>
								<TD style="WIDTH: 110px" align="left">�����û�<BR>
									<asp:textbox id="txtUser" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left"><asp:label id="Label2" runat="server">�û�IP</asp:label><FONT face="����"><BR>
									</FONT>
									<asp:textbox id="txtIP" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��ʼ����<BR>
									<asp:textbox id="txtStartDate" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��������<BR>
									<asp:textbox id="txtEndDate" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��ԴID<BR>
									<asp:textbox id="txtResID" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��¼ID<BR>
									<asp:textbox id="txtRecID" runat="server" Width="110px"></asp:textbox></TD>
								<td vAlign="bottom"></td>
							</tr>
							<TR>
								<TD style="WIDTH: 110px" align="left">��չ�ֶ�1<BR>
									<asp:textbox id="txtExt1" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��չ�ֶ�2<BR>
									<asp:textbox id="txtExt2" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��չ�ֶ�3<BR>
									<asp:textbox id="txtExt3" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��չ�ֶ�4<BR>
									<asp:textbox id="txtExt4" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��չ�ֶ�5<BR>
									<asp:textbox id="txtExt5" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">��չ�ֶ�6<BR>
									<asp:textbox id="txtExt6" runat="server" Width="110px"></asp:textbox></TD>
								<TD style="WIDTH: 110px" align="left">���ؼ�¼��<BR>
									<asp:textbox id="txtRowsCount" runat="server" Width="110px">500</asp:textbox></TD>
							</TR>
							<TR>
								<TD colSpan="7"></TD>
							</TR>
						</table>
						<table cellSpacing="0" cellPadding="4" border="0">
							<TR>
								<TD vAlign="bottom" noWrap align="left" width="200"><IMG src="/cmsweb/images/icons/xpLens.gif" align="absMiddle" border="0" width="16" height="16">&nbsp;
									<asp:linkbutton id="lnkQuery" runat="server">��ϲ�ѯ</asp:linkbutton>&nbsp;&nbsp;
									<asp:linkbutton id="lnkCancel" runat="server">ȡ����ѯ</asp:linkbutton>&nbsp;&nbsp;
								</TD>
								<TD vAlign="bottom" noWrap align="left" width="300"><PAGER:CMSPAGER id="Cmspager1" runat="server"></PAGER:CMSPAGER></TD>
								<TD width="1520"></TD>
							</TR>
							<TR style="display:none">
								<TD colSpan="3"><IMG src="/cmsweb/images/del.gif" align="absMiddle" border="0" width="20" height="19">
									<asp:linkbutton id="lbtnDelete" runat="server" Font-Bold="True" ToolTip="��ɾ����ǰ��ѯ���">ɾ����־</asp:linkbutton>&nbsp;��ɾ����ǰ��ѯ����е�������־��ֻ��ϵͳ��ȫԱ��ɾ����־��Ȩ�ޣ�</TD>
							</TR>
							<TR>
								<TD colSpan="3" height="4"></TD>
							</TR>
							<TR>
								<TD colSpan="3"><asp:datagrid id="DataGrid1" runat="server" CssClass="table_level2" width="2020px" AutoGenerateColumns="False">
										<ItemStyle Height="22px"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="22px" BackColor="#E7EBEF"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="LOG_TITLE" HeaderText="��־����">
												<ItemStyle Width="100px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_RESNAME" HeaderText="��Դ����">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_CONTENT" HeaderText="��־����">
												<ItemStyle Width="350px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_USERNAME" HeaderText="�����û�">
												<ItemStyle Width="100px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_CLIENTIP" HeaderText="�û�IP">
												<ItemStyle Width="100px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_TIME" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
												<ItemStyle Width="120px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_RESID" HeaderText="��ԴID">
												<ItemStyle Width="100px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_RECID" HeaderText="��¼ID">
												<ItemStyle Width="100px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_REF1" HeaderText="��չ�ֶ�1">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_REF2" HeaderText="��չ�ֶ�2">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_REF3" HeaderText="��չ�ֶ�3">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_REF4" HeaderText="��չ�ֶ�4">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_REF5" HeaderText="��չ�ֶ�5">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="LOG_REF6" HeaderText="��չ�ֶ�6">
												<ItemStyle Width="150px"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle Visible="False"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
