<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Cms.Web.ResourceSyncColumn" CodeFile="ResourceSyncColumn.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
    <title>数据同步</title>
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
	self.document.forms(0).RECID.value = src.RECID; //需要将用户选择的行号POST给服务器
}
-->
    </script>
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <input type="hidden" name="RECID">
      <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
          <td width="4"></td>
          <td>
            <TABLE class="table_level2" style="WIDTH: 664px" cellSpacing="0" cellPadding="0" width="664"
              border="0">
              <TR>
                <TD class="header_level2" style="WIDTH: 557px" colSpan="2" height="10"><b>数据同步－字段定义</b></TD>
              </TR>
              <TR height="23">
                <td style="WIDTH: 557px" vAlign="top" colSpan="2" height="22">
                  <TABLE style="WIDTH: 652px" cellSpacing="0" cellPadding="0" width="652" border="0">
                    <TR>
                      <TD style="WIDTH: 240px; HEIGHT: 12px" vAlign="bottom" colSpan="2"></TD>
                    </TR>
                    <TR height="25">
                      <TD style="WIDTH: 240px" vAlign="bottom"><FONT face="宋体"><asp:label id="Label1" runat="server">主资源：</asp:label><asp:label id="lblHostResName" runat="server"></asp:label></FONT></TD>
                      <TD style="WIDTH: 240px" vAlign="bottom"><FONT face="宋体"><asp:label id="Label2" runat="server">同步资源：</asp:label><asp:label id="lblSyncResName" runat="server"></asp:label></FONT></TD>
                    </TR>
                    <tr>
                      <td style="WIDTH: 240px" vAlign="top"><FONT face="宋体"><asp:listbox id="ListBox1" runat="server" Height="245px" Width="324px"></asp:listbox></FONT></td>
                      <td style="WIDTH: 240px" vAlign="top"><FONT face="宋体"><asp:listbox id="ListBox2" runat="server" Height="245px" Width="324px"></asp:listbox></FONT></td>
                    </tr>
                    <TR>
                      <TD vAlign="top" align="left" colSpan="2" height="28"><asp:button id="btnAddHostCol" runat="server" Width="108px" Text="添加主关联字段"></asp:button><asp:button id="btnAddSyncCol" runat="server" Width="92px" Text="添加同步字段"></asp:button>&nbsp;&nbsp;<asp:button id="btnAddCondCol" runat="server" Width="92px" Text="添加条件字段"></asp:button>
                        <asp:button id="btnAddConstCol" runat="server" Width="92px" Text="添加常量字段"></asp:button><FONT face="宋体"><asp:textbox id="txtCondVal" runat="server" Width="116px"></asp:textbox></FONT>&nbsp;&nbsp;
                        <asp:button id="btnExit" runat="server" Width="80px" Text="退出"></asp:button></TD>
                    </TR>
                    <TR>
                      <TD style="WIDTH: 732px" vAlign="top" colSpan="2"><FONT face="宋体"><asp:datagrid id="DataGrid1" runat="server" Width="390px"></asp:datagrid></FONT></TD>
                    </TR>
                    <TR>
                      <TD style="WIDTH: 732px" vAlign="top" colSpan="2" height="12"></TD>
                    </TR>
                    <TR>
                      <TD style="WIDTH: 732px" vAlign="top" colSpan="2"><IMG src="/cmsweb/images/Icons/up.gif" align="absMiddle" border="0" width="16" height="16">
                        <asp:linkbutton id="lbtnMoveUp" runat="server">向上移动</asp:linkbutton>&nbsp;<IMG src="/cmsweb/images/Icons/down.gif" align="absMiddle" border="0" width="16" height="16">
                        <asp:linkbutton id="lbtnMoveDown" runat="server">向下移动</asp:linkbutton></TD>
                    </TR>
                  </TABLE>
                </td>
              </TR>
            </TABLE>
          </td>
        </tr>
      </TABLE>
    </form>
  </body>
</HTML>
