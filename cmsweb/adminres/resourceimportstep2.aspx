<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceImportStep2" CodeFile="ResourceImportStep2.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>�����ⲿ���ݱ�������3</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" encType="multipart/form-data" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" style="WIDTH: 792px" cellSpacing="0" cellPadding="0" width="792"
							border="0">
							<TR>
								<TD class="header_level2" style="WIDTH: 457px" height="22"><b>�����ⲿ���ݱ�</b></TD>
							</TR>
							<TR>
								<TD align="left" style="HEIGHT: 15px"><FONT face="����"> </FONT>
								</TD>
							</TR>
							<TR height="23">
								<TD style="WIDTH: 457px">
									<TABLE style="WIDTH: 776px" cellSpacing="0" cellPadding="0" width="776" border="0">
										<TR>
											<TD style="WIDTH: 272px; HEIGHT: 12px" vAlign="top" align="left"><FONT face="����"><FONT face="����"><FONT face="����">��ѡ�����</FONT></FONT>
													<asp:DropDownList id="ddlTableList" runat="server" Width="168px"></asp:DropDownList></FONT></TD>
											<TD style="HEIGHT: 12px" vAlign="top"><FONT face="����">
													<asp:CheckBox id="chkImportData" runat="server" Text="���������" Checked="True"></asp:CheckBox>&nbsp;
													<asp:Button id="btnConfirm" runat="server" Text="��ʼ����" Width="80px"></asp:Button>
													<asp:Button id="btnExit" runat="server" Text="�˳�" Width="56px"></asp:Button></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 272px; HEIGHT: 18px" vAlign="top" align="left"><FONT face="����"><FONT face="����">
													</FONT></FONT>
											</TD>
											<TD style="HEIGHT: 18px" vAlign="top"></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 272px; HEIGHT: 18px" vAlign="top" align="left"><FONT face="����"><FONT face="����">
														Դ���ֶΣ�</FONT></FONT></TD>
											<TD style="HEIGHT: 18px" align="left" vAlign="top"><FONT face="����"><FONT face="����">
														<asp:Button id="btnAddSrcDestField" runat="server" Text="ƥ��Դ/Ŀ���ֶ�"></asp:Button>
														<asp:Button id="btnAddSrcField" runat="server" Text="�½�Դ�ֶ�"></asp:Button>
														<asp:Button id="btnAddAllSrcField" runat="server" Text="�½�����Դ�ֶ�"></asp:Button></FONT></FONT></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 272px; HEIGHT: 18px" vAlign="top" align="left"><FONT face="����">
													<asp:ListBox id="ListBox1" runat="server" Width="260px" Height="240px"></asp:ListBox><BR>
													<FONT face="����"><FONT face="����">Ŀ����Դ��
															<asp:Label id="lblResName" runat="server"></asp:Label>����</FONT></FONT><BR>
													<asp:ListBox id="ListBox2" runat="server" Width="260px" Height="240px"></asp:ListBox></FONT></TD>
											<TD style="HEIGHT: 18px" vAlign="top" align="left">
												<asp:DataGrid id="DataGrid1" runat="server"></asp:DataGrid></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
