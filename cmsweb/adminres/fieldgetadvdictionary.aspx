<%@ Register TagPrefix="Pager" NameSpace="Unionsoft.Cms.Web"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.FieldGetAdvDictionary" validateRequest="false" CodeFile="FieldGetAdvDictionary.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML dir="ltr">
	<HEAD>
		<TITLE id="onetidTitle">高级字典</TITLE>
		<META content="MSHTML 6.00.3790.0" name="GENERATOR">
		<META http-equiv="expires" content="0">
		<LINK href="/cmsweb/css/cmsstyle.css" type="text/css" rel="stylesheet">
	</HEAD>
	<BODY>
		<FORM id="Form1" name="Form1" action="" method="post" runat="server">
			<input type="hidden" name="RECID">
			<TABLE style="PADDING-LEFT: 4px" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE class="toolbar_table" cellSpacing="0" border="0">
							<TR>
								<TD width="8"></TD>
								<TD noWrap align="left" width="75"><IMG src="/cmsweb/images/icons/xpFolder.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:hyperlink id="lnkOK" runat="server" NavigateUrl="#" onclick="javascript:return ReturnSelectedValue();">确认</asp:hyperlink></TD>
								<TD noWrap align="left" width="71"><IMG src="/cmsweb/images/icons/xpFolder.gif" align="absMiddle" width="16" height="16">&nbsp;<asp:hyperlink id="lnkCancel" runat="server" NavigateUrl="#" onclick="window.close();">取消</asp:hyperlink></TD>
								<td noWrap width="100px" align="right"><asp:Image ID="imgEdit" runat="server" ImageUrl='/cmsweb/images/titleicon/creat.gif' align="absMiddle" Visible=false   /> <asp:LinkButton ID="lbtnAdd" runat="server" Visible=false >添加</asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton ID="lbtnEdit" runat="server" Visible=false >修改</asp:LinkButton></td>
								<td width="100%"></td>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td align="left" height="7"><asp:dropdownlist id="ddlColumns" runat="server" Width="148px"></asp:dropdownlist><asp:dropdownlist id="ddlConditions" runat="server" Width="88px"></asp:dropdownlist><asp:textbox id="txtSearchValue" runat="server" Width="184px"></asp:textbox>&nbsp;&nbsp;&nbsp;<asp:linkbutton id="lbtnSearch" runat="server">开始查询</asp:linkbutton>&nbsp;&nbsp;<asp:linkbutton id="lbtnCancelSearch" runat="server">取消查询</asp:linkbutton></td>
				</tr>
				<TR>
					<TD align="left" height="25">
						<Pager:CmsPager id="Cmspager1" runat="server"></Pager:CmsPager>
					</TD>
				</TR>
				<TR>
					<TD align="left" height="2"></TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<asp:datagrid id="DataGrid1" runat="server">
							<PagerStyle Visible="False"></PagerStyle>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			<asp:TextBox ID="txtRecID" runat='server' style="display:none;"></asp:TextBox>
		</FORM>
	</BODY>
</HTML> 