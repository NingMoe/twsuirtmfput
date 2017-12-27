<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.SysClientCode" CodeFile="SysClientCode.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<TITLE id=onetidTitle>系统选项</TITLE>
<META content="MSHTML 6.00.3790.0" name=GENERATOR>
<META http-equiv=expires content=0><LINK href="/cmsweb/css/cmsstyle.css" type=text/css rel=stylesheet >
</HEAD>
<BODY>
<SCRIPT>
		</SCRIPT>

<FORM id=Form1 name=Form1 action="" method=post 
runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
  <tr>
    <td width=4></td>
    <td>
      <TABLE class=table_level2 cellSpacing=0 cellPadding=0 width=450 border=0>
        <TR>
          <TD class=header_level2 width=450 colSpan=2 height=22 ><b>设置客户编码</b></TD></TR>
        <TR height=5>
          <td align=right width=130 height=20></td>
          <TD width="320" height=20></TD></TR>
        <TR>
          <TD align=right width=130 height=25><asp:label id=Label13 runat="server">客户编码</asp:label>&nbsp;</TD>
          <TD width="320" height=25><asp:textbox id=txtClientCode runat="server" Width="160px"></asp:textbox>&nbsp;</TD></TR>
        <TR>
          <TD align=right width=130 height=15></TD>
          <TD width=320 height=15></TD></TR>
        <TR>
          <TD align=right width=130 height=25></TD>
          <TD width="320" height=25><asp:Button id=btnConfirm runat="server" Text="保存" Width="88px"></asp:Button><asp:Button id=btnCancel runat="server" Text="退出" Width="88px"></asp:Button></TD></TR></TABLE></td></tr></TABLE></FORM>
	</BODY>
</HTML>
