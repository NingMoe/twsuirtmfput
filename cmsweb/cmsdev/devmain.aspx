<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.DevMain" CodeFile="DevMain.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE 
id=onetidTitle>ϵͳ���ߣ���ҳ</TITLE>
		<meta http-equiv="Pragma" content="no-cache" >
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
  </HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" method="post" runat="server">
			<TABLE  width="100%" border="0">
				<TBODY>
					<TR>
						<TD width="400" valign="top">
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0" width="400" border="0">
								<TR>
									<TD class="header_level2" align="center" height="19">ϵͳ����</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="36"><asp:linkbutton id="lbtnExit" runat="server" ForeColor="Red">�˳�����ϵͳ</asp:linkbutton></TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnAppConfig2" runat="server">ϵͳ������Ϣ</asp:linkbutton>
									</TD> 
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnSwitch" runat="server">ϵͳ���ܿ���</asp:linkbutton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:LinkButton id="lbtnDebug" runat="server">ϵͳ����</asp:LinkButton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnSysConfig" runat="server">ϵͳ�ֶ��߼�����</asp:linkbutton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnGetLog" runat="server">��ȡ������־</asp:linkbutton>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnUpdateDb" runat="server">����SQL���ݿ�</asp:linkbutton>
										<asp:CheckBox id="chkCheckLastUpdateTime" runat="server" Text="������������ڿ�ʼ���£������ͷ��ʼ���£�" Checked="True"></asp:CheckBox>
									</TD>
								</TR>
								<TR align="center" height="30" style="DISPLAY:none">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnUpdateMdb" runat="server">����MDB���ݿ�</asp:linkbutton>
									</TD>
								</TR>
								<TR align="center" height="30">
									<TD vAlign="middle" align="left" height="28">
										<asp:linkbutton id="lbtnGetUpdateTime" runat="server">��ȡ���¸���ʱ��</asp:linkbutton>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="26">
										<asp:linkbutton id="Linkbutton1" runat="server" OnClick="ExchangeDocumentCenter">[�¹��ܸ���]�ĵ��ܹ�����ڶ�����ݿ���</asp:linkbutton>
									</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="26">
										<span style="color:Red;">*</span><a href="../UpdatePwd.aspx" target="_blank" > ����������ܷ�ʽ</a>
									</TD>
								</TR>
							</TABLE>
						</TD>
						<td valign="top">
							<TABLE class="table_level2" cellSpacing="0" cellPadding="0"  border="0" width="100%">
								<TR>
									<TD class="header_level2" align="center" height="19">ϵͳ��Ϣ</TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="28"><asp:textbox id="txtSysError" runat="server" Height="228px" Width="610" TextMode="MultiLine"></asp:textbox></TD>
								</TR>
								<TR>
									<TD vAlign="middle" align="left" height="28"><asp:linkbutton id="lbtnClearNotes" runat="server">���ϵͳ��Ϣ</asp:linkbutton></TD>
								</TR>
							</TABLE>
						</td>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
