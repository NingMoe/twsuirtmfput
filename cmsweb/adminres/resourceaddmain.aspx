<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceAddMain" CodeFile="ResourceAddMain.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE id="onetidTitle">������Դ</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<SCRIPT>
		</SCRIPT>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td width="4"></td>
					<td>
						<TABLE class="form_table" cellSpacing="0" cellPadding="0">
							<TR>
								<TD height="22" colspan="2" class="header_level2"><b>������Դ����</b></TD>
							</TR>
							<TR>
								<TD align="right" width="150" height="12">
								</TD>
								<TD height="12"></TD>
							</TR>
							<TR height="22">
								<td width="150" align="right">
									<asp:Label id="Label1" runat="server">��Դ���ƣ�</asp:Label></td>
								<TD>
									<asp:TextBox id="txtResName" runat="server" Width="228px"></asp:TextBox>
									<asp:CheckBox id="chkInherit" runat="server" Text="�̳�����Դ"></asp:CheckBox>
								</TD>
							</TR>
							<TR>
								<TD align="right" width="150" height="2">
									<asp:Label id="Label2" runat="server">�����ͣ�</asp:Label></TD>
								<TD height="2">
									<asp:DropDownList id="ddlResType" runat="server" Width="228px"></asp:DropDownList>
									<asp:Label id="lblResTableType" runat="server">���ÿ��򴴽�����Դ��</asp:Label></TD>
							</TR>
							<TR>
								<TD align="right" width="150">
									<asp:Label id="lblTemplate" runat="server">������ģ�壺</asp:Label></TD>
								<TD>
									<asp:DropDownList id="ddlTemplate" runat="server" Width="228px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD align="right" width="150">
									<asp:Label id="lblTableName" runat="server">�Զ�������ƣ�</asp:Label></TD>
								<TD>
									<asp:TextBox id="txtTableName" runat="server" Width="228px"></asp:TextBox>
									<asp:Label id="lblComments" runat="server">���߼�ѡ�ϵͳʵʩ��Աר�ã�</asp:Label></TD>
							</TR> 
							<TR>
								<TD width="150" height="12"></TD>
								<TD height="12"></TD>
							</TR>
							<TR height="22">
								<TD width="150"></TD>
								<TD>
									<asp:Button id="btnSaveResource" runat="server" Width="80px" Text="ȷ��"></asp:Button>
									<asp:Button id="btnCancle" runat="server" Width="80px" Text="ȡ��"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</FORM>
	</BODY>
</HTML>
