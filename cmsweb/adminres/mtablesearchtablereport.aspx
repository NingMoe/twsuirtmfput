<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.MTableSearchTableReport" validateRequest="false" CodeFile="MTableSearchTableReport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ͳ�Ʊ�����</title>
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
								<TD class="form_header" colSpan="2"><b><asp:label id="lblTitle" runat="server">ͳ�Ʊ�����</asp:label></b></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="lblHeader" runat="server">��ͷ��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtHeader" runat="server" Height="60px" TextMode="MultiLine" Width="600px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label1" runat="server">��ͷԪ��1��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF1" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label6" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF1Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label10" runat="server">��</asp:label><asp:dropdownlist id="ddlF1" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label14" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc1" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol1" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label2" runat="server">��ͷԪ��2��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF2" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label7" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF2Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label11" runat="server">��</asp:label><asp:dropdownlist id="ddlF2" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label15" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc2" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol2" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label3" runat="server">��ͷԪ��3��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF3" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label8" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF3Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label12" runat="server">��</asp:label><asp:dropdownlist id="ddlF3" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label16" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc3" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol3" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px; HEIGHT: 25px" align="right"><asp:label id="Label4" runat="server">��ͷԪ��4��</asp:label></TD>
								<TD align="left" style="HEIGHT: 25px"><asp:textbox id="txtF4" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label9" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF4Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label13" runat="server">��</asp:label><asp:dropdownlist id="ddlF4" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label17" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc4" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol4" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label18" runat="server">��ͷԪ��5��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF5" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label19" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF5Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label20" runat="server">��</asp:label><asp:dropdownlist id="ddlF5" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label21" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc5" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol5" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label22" runat="server">��ͷԪ��6��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF6" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label23" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF6Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label24" runat="server">��</asp:label><asp:dropdownlist id="ddlF6" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label25" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc6" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol6" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label26" runat="server">��ͷԪ��7��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF7" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label27" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF7Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label28" runat="server">��</asp:label><asp:dropdownlist id="ddlF7" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label29" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc7" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol7" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label30" runat="server">��ͷԪ��8��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF8" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label31" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF8Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label32" runat="server">��</asp:label><asp:dropdownlist id="ddlF8" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label33" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc8" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol8" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label34" runat="server">��βԪ��9��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF9" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label35" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF9Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label36" runat="server">��</asp:label><asp:dropdownlist id="ddlF9" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label37" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc9" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol9" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label38" runat="server">��βԪ��10��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtF10" runat="server" Width="100px"></asp:textbox><FONT face="����">&nbsp;</FONT>
									<asp:label id="Label39" runat="server">��ֵΪ</asp:label><asp:textbox id="txtF10Val" runat="server" Width="100px"></asp:textbox><asp:label id="Label40" runat="server">��</asp:label><asp:dropdownlist id="ddlF10" runat="server" Width="104px"></asp:dropdownlist><asp:label id="Label41" runat="server">��</asp:label><asp:dropdownlist id="ddlFunc10" runat="server" Width="72px"></asp:dropdownlist>
									<asp:DropDownList id="ddlCol10" runat="server" Width="152px"></asp:DropDownList></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label5" runat="server">��β��</asp:label></TD>
								<TD align="left"><asp:textbox id="txtTail" runat="server" Height="124px" TextMode="MultiLine" Width="604px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right"><asp:label id="Label44" runat="server">ҳ�������</asp:label></TD>
								<TD align="left">
									�иߣ�<asp:TextBox id="txtRowHeight" runat="server" Width="100px" Text="20"></asp:TextBox>
									������<asp:TextBox id="txtRowCount" runat="server" Width="100px" Text="15"></asp:TextBox>
								</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px; HEIGHT: 12px" align="right"></TD>
								<TD style="HEIGHT: 12px" align="left"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 90px" align="right">&nbsp;</TD>
								<TD align="left"><asp:Button id="btnConfirm" runat="server" Width="80px" Text="��������"></asp:Button>
									<asp:Button id="btnClear" runat="server" Width="80px" Text="�������"></asp:Button><asp:Button id="btnExit" runat="server" Width="80px" Text="�˳�"></asp:Button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
