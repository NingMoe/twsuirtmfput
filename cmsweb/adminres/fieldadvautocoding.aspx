<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldAdvAutoCoding" validateRequest="false" CodeFile="FieldAdvAutoCoding.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>FieldAdvAutoCoding</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
		<script language="javascript">
<!--
function RowLeftClickPost(src){
	var o=src.parentNode;
	for (var k=1;k<o.children.length;k++){
		o.children[k].bgColor = "white";
	}
	
	self.document.forms(0).acodeaction.value = "rowclick"; //��Ҫ���û�ѡ����к�POST��������
	self.document.forms(0).RECID.value = src.RECID; //��Ҫ���û�ѡ����к�POST��������
	self.document.forms(0).submit();
}
//-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input type="hidden" name="acodeaction"> <input type="hidden" name="RECID">
			<TABLE cellSpacing="0" cellPadding="0" width="504" border="0" style="WIDTH: 504px">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="490" border="0">
							<TR>
								<TD class="header_level2" colSpan="2" height="22"><b>�Զ��������� ����Դ��[
										<asp:label id="lblResName" runat="server"></asp:label>]&nbsp; �ֶΣ�[
										<asp:label id="lblFieldName" runat="server"></asp:label>]��</b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><asp:datagrid id="DataGrid1" runat="server" Width="456px"></asp:datagrid></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><FONT face="����"><BR>
										<BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5">
									<table class="table_level3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="header_level3" colSpan="2" height="20">��������</td>
										</tr>
										<tr>
											<td height="20"><asp:textbox id="txtSNumConstant" runat="server"></asp:textbox><asp:button id="btnSNumAddConstant" runat="server" Text="��ӳ���"></asp:button><asp:button id="btnSNumEditConstant" runat="server" Text="�޸ĳ���"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><FONT face="����"><BR>
										<BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5">
									<table class="table_level3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="header_level3" colSpan="2" height="20">��������</td>
										</tr>
										<tr>
											<td height="20"><asp:radiobutton id="rdoSNumYear1" runat="server" Text="YYYY(��)" GroupName="GrpSNumDate"></asp:radiobutton><asp:radiobutton id="rdoSNumYear2" runat="server" Text="YY(��)" GroupName="GrpSNumDate"></asp:radiobutton><asp:radiobutton id="rdoSNumMonth" runat="server" Text="MM(��)" GroupName="GrpSNumDate"></asp:radiobutton><asp:radiobutton id="rdoSNumDate" runat="server" Text="DD(��)" GroupName="GrpSNumDate"></asp:radiobutton><asp:button id="btnSNumAddDate" runat="server" Text="�������"></asp:button><asp:button id="btnSNumEditDate" runat="server" Text="�޸�����"></asp:button></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5"><FONT face="����"><BR>
										<BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px" height="5">
									<!--��ˮ�����ÿ�ʼ-->
									<table class="table_level3" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="header_level3" colSpan="2" height="20">��ˮ������</td>
										</tr>
										<tr>
											<td height="20">����ʱ�䣺&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumNoReset" runat="server" Text="������" GroupName="GrpSNumResetTime"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumYearReset" runat="server" Text="ÿ��" GroupName="GrpSNumResetTime"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumMonthReset" runat="server" Text="ÿ��" GroupName="GrpSNumResetTime"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rdoSNumDateReset" runat="server" Text="ÿ��" GroupName="GrpSNumResetTime"></asp:radiobutton></td>
										</tr>
										<tr>
											<td height="20">��ˮ��λ����&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:textbox id="txtSNumDigitNum" runat="server" Width="48px" MaxLength="2"></asp:textbox><asp:checkbox id="chkSNumPreZero" runat="server" Text="λ��������ǰ����0����"></asp:checkbox></td>
										</tr>
										<tr>
											<td height="20">�������������֣�
												<asp:textbox id="txtNumToSkip" runat="server" Width="56px"></asp:textbox>(��ʽ��4 
												�� 4,7)
											</td>
										</tr>
										<TR>
											<TD height="20"><FONT face="����"><FONT face="����">��ǰ��ˮ��ֵ��&nbsp;&nbsp;
														<asp:textbox id="txtSNumValue" runat="server" Width="64px"></asp:textbox></FONT></FONT></TD>
										</TR>
										<tr>
											<td height="20"><asp:button id="btnSNumAddSNum" runat="server" Text="�����ˮ������"></asp:button><asp:button id="btnSNumEditSNum" runat="server" Text="�޸���ˮ������"></asp:button></td>
										</tr>
									</table> <!--��ˮ�����ý���--></TD>
							</TR>
							<TR height="22">
								<TD style="WIDTH: 480px"><FONT face="����"><BR>
									</FONT>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 480px"><FONT face="����"><asp:button id="btnExit" runat="server" Width="104px" Text="�˳�"></asp:button></FONT></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
