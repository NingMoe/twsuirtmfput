<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DepartmentManager" CodeFile="DepartmentManager.aspx.vb" %>
<%@ Import NameSpace="Unionsoft.Cms.Web"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<TITLE>���Ź���</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmstree.css" type="text/css" rel="stylesheet">
			<script language="JavaScript" src="/cmsweb/script/CmsTreeview.js"></script>
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" method="post" runat="server">
			<TABLE style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; PADDING-BOTTOM: 2px; PADDING-TOP: 1px"
				width="100%" border="0">
				<TBODY>
					<TR>
						<TD vAlign="middle" width="240" class="toolbar_table_dep">&nbsp;���Ź���</TD>
						<TD>
							<TABLE class="toolbar_table" cellSpacing="0" border="0">
								<TR>
									<TD noWrap align="left" width="4"></TD>
									<TD noWrap align="left" width="99"><IMG src="/cmsweb/images/titleicon/department_del.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:linkbutton id="lbtnDelDep" runat="server">ɾ������</asp:linkbutton></TD>
									<TD noWrap align="left" width="90"><IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" width="16" height="16">
										<asp:linkbutton id="lbtnDepMoveup" runat="server">�����ƶ�</asp:linkbutton></TD>
									<TD noWrap align="left" width="97"><IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:linkbutton id="lbtnDepMovedown" runat="server" DESIGNTIMEDRAGDROP="156">�����ƶ�</asp:linkbutton></TD>
									<TD noWrap align="left" width="109"><IMG src="/cmsweb/images/titleicon/department_move.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:linkbutton id="lbtnDepMove" runat="server">�ƶ�����</asp:linkbutton></TD>
									<TD>&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR> 
						<TD vAlign="top" width="240" height="100%" rowSpan="7"><asp:Panel id="panelDepTree" style="OVERFLOW: auto" Width="240" Height="100%" runat="server"><%WebTreeDepartment.LoadResTreeView(CmsPass, Request, Response, "/cmsweb/adminsys/DepartmentManager.aspx", "_self",unionsoft.Platform .AspPage.RStr("depid", Request),CmsPass.Employee.ID)%></asp:Panel>
						</TD>
						<TD valign="top">
							<TABLE cellSpacing="0" cellPadding="0" width="420" border="0">
								<TR>
									<TD width="420">
										<FIELDSET style="HEIGHT: 70px" DESIGNTIMEDRAGDROP="97">
											<LEGEND>���Ӳ���</LEGEND>
											<asp:textbox id="txtDepName" runat="server" Width="200" EnableViewState="False"></asp:textbox>
											<asp:checkbox id="chkVirtualDep" runat="server" EnableViewState="False" Text="���ⲿ��"></asp:checkbox>
											<BR>
											<asp:button id="btnDepAdd" runat="server" Text="���Ӳ���"></asp:button>
										</FIELDSET><BR>
									</TD>
								</TR>
							</TABLE>
							<table cellSpacing="0" cellPadding="0" width="420" border="0">
								<tr height="10">
									<td>
										<FIELDSET style="HEIGHT: 70px" DESIGNTIMEDRAGDROP="2091">
											<LEGEND>�޸���ҵ��������</LEGEND>
											<asp:textbox id="txtDepNameToEdit" runat="server" Width="200" EnableViewState="False"></asp:textbox>
											
											<asp:CheckBox id="chkShowEnable" runat="server" Text="�����ݹ������ṹ����ʾ"></asp:CheckBox>
											<FONT face="����">
												<BR>
											</FONT>
											<asp:button id="btnDepEdit" runat="server" Text="�޸�����"></asp:button>
										</FIELDSET><BR>
									</td>
								</tr>
								<tr>
									<td>
										<FIELDSET style="HEIGHT: 70px" DESIGNTIMEDRAGDROP="106"><legend>���Ź���Ա����</legend>��ǰ���Ź���Ա��<asp:label id="lblDepAdminID" runat="server"></asp:label><BR><BR><asp:button id="btnSetDepAdmin" runat="server" Text="���ù���Ա"></asp:button><asp:button id="btnDelDepAdmin" runat="server" Text="��չ���Ա"></asp:button></FIELDSET>
										<BR>
									</td>
								</tr>
								<TR>
									<TD>
										<FIELDSET style="HEIGHT: 70px"><LEGEND>��������</LEGEND>��ǰ�������ͣ� <asp:label id="lblDepType" runat="server"></asp:label><BR><BR></FIELDSET></TD>
								</TR>
								<tr>
								<td>
								<FIELDSET style="HEIGHT: 70px" DESIGNTIMEDRAGDROP="106" id="ADSetup" runat=server><legend>������AD�еĶ�Ӧ����</legend>
							
								<input type=Text id="TextAD_OU" runat=Server  style="width:200px" /><asp:Button runat=server ID="BtnSaveAD_OU" Text="����" />
								</FIELDSET>
								</td>
								</tr>
							</table>
						</TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD></TD>
					</TR>
					<TR>
						<TD height="100%">&nbsp;</TD>
					</TR>
				</TBODY></TABLE>
		</FORM>
	</BODY>
</HTML>
<script language="javascript">
 document.getElementById("panelDepTree").style.height = document.documentElement.offsetHeight-40;
</script>